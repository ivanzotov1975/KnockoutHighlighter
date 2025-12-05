using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace KnockoutHighlighter.DataBind
{
  /// <summary>
  ///   Classifier that classifies all text as an instance of the "EditorClassifier" classification type.
  /// </summary>
  internal class EditorClassifier : IClassifier
  {
    //REGEX
    private static readonly Regex DataBindRegex = new Regex(@"data-bind\s*=\s*""([^""]*)""", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex BracketRegex = new Regex(@"[\[\]\(\)\{\}]", RegexOptions.Compiled);
    private readonly Regex _keywordRegex;
    //END REGEX

    private readonly IClassificationType _bracketType;
    private readonly IClassificationType _keywordType;
    private readonly IClassificationType _nameType;
    private readonly IClassificationType _valueType;
    private readonly IClassificationType _commentType;
    private readonly ITextBuffer _buffer;

    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorClassifier" /> class.
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="registry">Classification registry.</param>
    internal EditorClassifier(ITextBuffer buffer, IClassificationTypeRegistryService registry)
    {
      _buffer = buffer;
      _nameType = registry.GetClassificationType("dataBindName");
      _valueType = registry.GetClassificationType("dataBindValue");
      _keywordType = registry.GetClassificationType("dataBindKeyword");
      _bracketType = registry.GetClassificationType("dataBindBracket");
      _commentType= registry.GetClassificationType("dataBindComment");

      _keywordRegex = BuildKeywordRegex();
    }
    public void Refresh()
    {
      var snapshot = _buffer.CurrentSnapshot;
      ClassificationChanged?.Invoke(this, new ClassificationChangedEventArgs(new SnapshotSpan(snapshot, 0, snapshot.Length)));
    }

    private Regex BuildKeywordRegex()
    {
      var options = KnockoutHighlighterPackage.Options;
      if(options == null) return new Regex(@"$^"); // match nothing

      var keywords = options.Keywords.Split(',')
        .Select(k => k.Trim())
        .Where(k => !string.IsNullOrWhiteSpace(k))
        .Select(Regex.Escape);

      var pattern = $@"\b({string.Join("|", keywords)})\s*:";
      return new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    #region IClassifier

#pragma warning disable 67

    /// <summary>
    ///   An event that occurs when the classification of a span of text has changed.
    /// </summary>
    /// <remarks>
    ///   This event gets raised if a non-text change would affect the classification in some way,
    ///   for example typing /* would cause the classification to change in C# without directly
    ///   affecting the span.
    /// </remarks>
    public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

    /// <summary>
    ///   Gets all the <see cref="ClassificationSpan" /> objects that intersect with the given range of text.
    /// </summary>
    /// <remarks>
    ///   This method scans the given SnapshotSpan for potential matches for this classification.
    ///   In this instance, it classifies everything and returns each span as a new ClassificationSpan.
    /// </remarks>
    /// <param name="span">The span currently being classified.</param>
    /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification.</returns>
    public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
    {
      //var spans = new List<ClassificationSpan>();
      //var text1 = span.GetText();
      //if(text1.TrimStart().StartsWith("//"))
      //{
      //  spans1.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), _textColorType));
      //}

      var snapshot = span.Snapshot;
      var fullSpan = new SnapshotSpan(snapshot, 0, snapshot.Length);
      var text = fullSpan.GetText();
      var spans = new List<ClassificationSpan>();

      //if(text.TrimStart().StartsWith("//"))
      //{
      //  spans.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), _textColorType));
      //}

      var matches = DataBindRegex.Matches(text);

      foreach(Match match in matches)
      {
        var nameGroup = match.Groups[0];
        var valueGroup = match.Groups[1];

        var attrStart = fullSpan.Start + nameGroup.Index;
        var attrNameLength = "data-bind".Length;
        var attrNameSpan = new SnapshotSpan(attrStart, attrNameLength);
        if(span.IntersectsWith(attrNameSpan)) spans.Add(new ClassificationSpan(attrNameSpan, _nameType));

        var valueStart = fullSpan.Start + valueGroup.Index;
        var valueSpan = new SnapshotSpan(valueStart, valueGroup.Length);

        if(!span.IntersectsWith(valueSpan))
          continue;

        spans.Add(new ClassificationSpan(valueSpan, _valueType));

        // Highlight keywords
        //var keywordRegex = BuildKeywordRegex();
        foreach(Match kw in _keywordRegex.Matches(valueGroup.Value))
        {
          var kwStart = valueStart + kw.Index;
          var kwSpan = new SnapshotSpan(kwStart, kw.Length);
          spans.Add(new ClassificationSpan(kwSpan, _keywordType));
        }

        // Highlight brackets
        foreach(Match br in BracketRegex.Matches(valueGroup.Value))
        {
          var brStart = valueStart + br.Index;
          var brSpan = new SnapshotSpan(brStart, 1);
          spans.Add(new ClassificationSpan(brSpan, _bracketType));
        }

        // Highlight comments
        var commentRegex = new Regex(@"(//.*?$|/\*.*?\*/)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.Multiline);
        foreach(Match cm in commentRegex.Matches(valueGroup.Value))
        {
          var cmStart = valueStart + cm.Index;
          var cmSpan = new SnapshotSpan(cmStart, cm.Length);
          spans.Add(new ClassificationSpan(cmSpan, _commentType));
        }


      }

      return spans;
    }

    #endregion
  }
}
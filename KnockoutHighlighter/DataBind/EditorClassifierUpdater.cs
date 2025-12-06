using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;

namespace KnockoutHighlighter.DataBind
{
  internal sealed class EditorClassifierUpdater
  {
    [Import] internal IClassificationTypeRegistryService ClassificationRegistry;

    [Import] internal IClassificationFormatMapService FormatMapService;

    public EditorClassifierUpdater()
    {
      KnockoutHighlighterPackage.Options.PropertyChanged += OnOptionsChanged;
    }

    private void OnOptionsChanged(object sender, PropertyChangedEventArgs e)
    {
      ThreadHelper.JoinableTaskFactory.Run(async () =>
      {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        foreach (var view in ViewTracker.ActiveViews)
        {
          ApplyUserStyles(view);
          if (view.TextBuffer.Properties.TryGetProperty(typeof(IClassifier), out IClassifier classifier) &&
              classifier is EditorClassifier knockoutHighlighterClassifier) knockoutHighlighterClassifier.Refresh();
        }
      });
    }

    public void ApplyUserStyles(IWpfTextView textView)
    {
      var formatMap = FormatMapService.GetClassificationFormatMap(textView);
      var options = KnockoutHighlighterPackage.Options;

      formatMap.BeginBatchUpdate();

      void SetStyle(string typeName, Color? fg, Color? bg, bool bold = false, bool italic = false)
      {
        var classification = ClassificationRegistry.GetClassificationType(typeName);
        var props = formatMap.GetTextProperties(classification);
        var newProps = props;

        if (fg.HasValue)
          newProps = newProps.SetForeground(
            System.Windows.Media.Color.FromArgb(fg.Value.A, fg.Value.R, fg.Value.G, fg.Value.B));
        if (bg.HasValue)
          newProps = newProps.SetBackground(
            System.Windows.Media.Color.FromArgb(bg.Value.A, bg.Value.R, bg.Value.G, bg.Value.B));
        if (bold)
          newProps = newProps.SetBold(true);
        if (italic)
          newProps = newProps.SetItalic(true);

        formatMap.SetTextProperties(classification, newProps);
      }

      formatMap.EndBatchUpdate();

      if (options == null) return;

      SetStyle("dataBindName", options.AttributeNameColor, null, options.BoldAttributeName);
      SetStyle("dataBindValue", null, options.ValueBackground);
      SetStyle("dataBindKeyword", options.KeywordForeground, null, italic: options.ItalicKeywords);
      SetStyle("dataBindSpecialKeyword", options.SpecialKeywordForeground, null, italic: options.ItalicKeywords);
      SetStyle("dataBindBracket", options.BracketColor, null);
      SetStyle("dataBindComment", options.CommentColor, null);
    }
  }
}
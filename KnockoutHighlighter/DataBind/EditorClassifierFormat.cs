using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace KnockoutHighlighter.DataBind
{

  /// <summary>
  /// Defines an editor format for the EditorClassifier types
  /// and is underlined.
  /// </summary>
  [Export(typeof(EditorFormatDefinition))]
  [ClassificationType(ClassificationTypeNames = "dataBindName")]
  [Name("dataBindName")]
  [UserVisible(true)]
  [Order(After = Priority.High)]
  internal sealed class DataBindNameFormat : ClassificationFormatDefinition
  {
    public DataBindNameFormat()
    {
      DisplayName = "data-bind name";
      ForegroundColor = Colors.MediumOrchid;
      //IsBold = false;
    }
  }

  [Export(typeof(EditorFormatDefinition))]
  [ClassificationType(ClassificationTypeNames = "dataBindValue")]
  [Name("dataBindValue")]
  [UserVisible(true)]
  [Order(After = Priority.High)]
  [Order(Before = "dataBindComment")]
  internal sealed class DataBindValueFormat : ClassificationFormatDefinition
  {
    public DataBindValueFormat()
    {
      DisplayName = "data-bind value";
      BackgroundColor = Colors.DarkBlue;
    }
  }

  [Export(typeof(EditorFormatDefinition))]
  [ClassificationType(ClassificationTypeNames = "dataBindKeyword")]
  [Name("dataBindKeyword")]
  [UserVisible(true)]
  [Order(After =Priority.High)]
  [Order(Before = "dataBindComment")]
  internal sealed class DataBindKeywordFormat : ClassificationFormatDefinition
  {
    public DataBindKeywordFormat()
    {
      DisplayName = "data-bind keyword";
      ForegroundColor = Colors.DarkTurquoise;
      IsItalic = false;
    }
  }

  [Export(typeof(EditorFormatDefinition))]
  [ClassificationType(ClassificationTypeNames = "dataBindBracket")]
  [Name("dataBindBracket")]
  [UserVisible(true)]
  [Order(After =Priority.High)]
  [Order(Before = "dataBindComment")]
  internal sealed class DataBindBracketFormat : ClassificationFormatDefinition
  {
    public DataBindBracketFormat()
    {
      DisplayName = "data-bind bracket";
      ForegroundColor = Colors.YellowGreen;
    }
  }

  [Export(typeof(EditorFormatDefinition))]
  [ClassificationType(ClassificationTypeNames = "dataBindComment")]
  [Name("dataBindComment")]
  [UserVisible(true)]
  [Order(After = Priority.High)]
  internal sealed class DataBindCommentFormat : ClassificationFormatDefinition
  {
    public DataBindCommentFormat()
    {
      DisplayName = "data-bind comments";
      ForegroundColor = Colors.Green;
    }
  }
}

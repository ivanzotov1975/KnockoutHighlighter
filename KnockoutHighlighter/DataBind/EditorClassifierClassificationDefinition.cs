using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace KnockoutHighlighter.DataBind
{
  /// <summary>
  /// Classification type definition export for EditorClassifier
  /// </summary>
  internal static class EditorClassifierClassificationDefinition
  {
    // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

    /// <summary>
    ///   Defines classification type.
    /// </summary>

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("dataBindName")]
    internal static ClassificationTypeDefinition DataBindNameType = null;

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("dataBindValue")]
    internal static ClassificationTypeDefinition DataBindValueType = null;

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("dataBindKeyword")]
    internal static ClassificationTypeDefinition DataBindKeywordType = null;

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("dataBindBracket")]
    internal static ClassificationTypeDefinition DataBindBracketType = null;

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("dataBindComment")]
    internal static ClassificationTypeDefinition DataBindCommentType = null;


#pragma warning restore 169
  }
}

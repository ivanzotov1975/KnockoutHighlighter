using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace KnockoutHighlighter.DataBind
{
  [Export(typeof(IWpfTextViewCreationListener))]
  [ContentType("text")]
  [TextViewRole(PredefinedTextViewRoles.Document)]
  internal sealed class ViewCreationListener : IWpfTextViewCreationListener
  {
    [Import] internal IClassificationTypeRegistryService ClassificationRegistry;
    [Import] internal IClassificationFormatMapService FormatMapService;

    public void TextViewCreated(IWpfTextView textView)
    {
      var updater = new EditorClassifierUpdater
      {
        ClassificationRegistry = ClassificationRegistry,
        FormatMapService = FormatMapService
      };

      ViewTracker.ActiveViews.Add(textView);

      updater.ApplyUserStyles(textView);

      // Trigger reclassification
      if (textView.TextBuffer.Properties.TryGetProperty(typeof(IClassifier), out IClassifier classifier))
        if (classifier is EditorClassifier editorClassifier)
          editorClassifier.Refresh();
    }
  }
}
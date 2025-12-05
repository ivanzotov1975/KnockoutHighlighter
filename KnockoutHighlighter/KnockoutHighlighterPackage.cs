using KnockoutHighlighter.DataBind;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace KnockoutHighlighter
{
 
  [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
  [InstalledProductRegistration("Knockout Highlighter", "Highlights Knockout 'data-bind' attribute value in HTML. With page dialog to adjust colors and fonts.", "1.0")]
  [ProvideOptionPage(typeof(OptionsPage), "Knockout Highlighter", "General", 0, 0, true)]
  [Guid(PackageGuidString)]
  [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
  public sealed class KnockoutHighlighterPackage : AsyncPackage
  {
    /// <summary>
    /// KnockoutHighlighterPackage GUID string.
    /// </summary>
    public const string PackageGuidString = "14c865fd-8c79-4c4d-a515-63372a561eb3";

    #region Package Members

    public static OptionsPage Options;

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    /// <param name="progress">A provider for progress updates.</param>
    /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
      // When initialized asynchronously, the current thread may be a background thread at this point.
      // Do any initialization that requires the UI thread after switching to the UI thread.
      await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

      //// Load saved settings
      var options = (OptionsPage)GetDialogPage(typeof(OptionsPage));
      Options = options;

    }

    #endregion
  }
}

using System.Windows;
using System.Windows.Threading;
using Netcorex.Shared.WPF.Localization;

namespace Netcorex.Shared.WPF.Windows
{
  /// <summary>
  /// Basis for Application(s) including handling unexpected errors
  /// </summary>
  public abstract class ApplicationBase : Application
  {
    public const string ProgramCompany = "NETCOREX";
    public const string ProgramCopyright = "Copyright © Martin Hlaváč 2013";


    protected ApplicationBase()
    {
      DispatcherUnhandledException += OnDispatcherUnhandledException;
    }


    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      MessageBox.Show(e.Exception.Message, SharedTitles.Error, MessageBoxButton.OK, MessageBoxImage.Error);
      e.Handled = true;
    }
  }
}

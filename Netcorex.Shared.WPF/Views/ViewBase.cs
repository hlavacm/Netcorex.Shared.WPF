using System.Windows.Controls;
using Netcorex.Shared.WPF.ViewModels;

namespace Netcorex.Shared.WPF.Views
{
  /// <summary>
  /// The common basis for Views
  /// </summary>
  public abstract class ViewBase : ContentControl
  {
    public new IViewModel DataContext
    {
      get { return base.DataContext as IViewModel; }
      set { base.DataContext = value; }
    }
  }
}

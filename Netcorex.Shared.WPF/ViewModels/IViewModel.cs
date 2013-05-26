using Netcorex.Shared.WPF.Models;

namespace Netcorex.Shared.WPF.ViewModels
{
  /// <summary>
  /// Basic universal access and definition of ViewModels
  /// </summary>
  public interface IViewModel
  {
    IModel Model { get; }
  }
}

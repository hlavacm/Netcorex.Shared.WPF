using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Netcorex.Shared.WPF.Common
{
  /// <summary>
  /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify MVVM etc.
  /// Based on WinRT samples...
  /// </summary>
  public abstract class BindableBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;


    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (Equals(storage, value))
        return false;
      storage = value;
      OnPropertyChanged(propertyName);
      return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler eventHandler = PropertyChanged;
      if (eventHandler == null)
        return;
      eventHandler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

using System;
using System.Windows.Input;

namespace Netcorex.Shared.WPF.Common
{
  /// <summary>
  /// This has been taken over from Josh Smith's MSDN-mag article on MVVM
  /// http://msdn.microsoft.com/en-us/magazine/dd419663.aspx
  /// http://www.blogs.intuidev.com/post/2010/02/15/WPF-TabControl-series-Part-4-Closeable-TabItems.aspx
  /// </summary>
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;


		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			_execute = execute;
			_canExecute = canExecute;
		}


		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}


		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}
	}
}

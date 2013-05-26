using System;
using System.Globalization;
using System.Windows.Data;

namespace Netcorex.Shared.WPF.Converters
{
  /// <summary>
  /// Helper converter for setting the size of the GUI elements
  /// </summary>
	public class PositionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			double position;
			if (double.TryParse(value.ToString(), out position))
			{
				if (position > 0)
				{
					return position;
				}
				return double.NaN;
			}
			throw new InvalidCastException(string.Format("The \"{0}\" is not double.", value));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}

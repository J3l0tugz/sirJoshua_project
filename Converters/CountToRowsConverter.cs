using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Mamilots_POS.Converters
{
    public class CountToRowsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return (int)Math.Ceiling(count / 3.0);  // Assuming 3 columns
            }

            return 1;  // Default to 1 row if count is not valid
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

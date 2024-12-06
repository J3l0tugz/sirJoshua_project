using System;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Mamilots_POS.Converters
{
    public class FilePathToUriConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string filePath)
            {
                return new Uri(filePath, UriKind.Absolute);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
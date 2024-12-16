using System;
using Avalonia.Data.Converters;
using System.Globalization;

namespace Mamilots_POS.Converters
{
    public class CategoryIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int categoryId)
            {
                return categoryId switch
                {
                    0 => "Taro Chips",
                    1 => "Banana Chips",
                    2 => "Camote Chips",
                    3 => "Other Products",
                    _ => "Unknown"
                };
            }
            return "Invalid";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("CategoryIdToNameConverter only supports one-way conversion.");
        }
    }
}

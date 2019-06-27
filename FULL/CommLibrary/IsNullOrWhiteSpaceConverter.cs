using System;
using System.Globalization;
using System.Windows.Data;

namespace CommLibrary
{
    public class IsNullOrWhiteSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value == null || string.IsNullOrWhiteSpace(value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

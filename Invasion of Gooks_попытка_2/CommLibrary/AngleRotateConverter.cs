using System;
using System.Globalization;
using System.Windows.Data;

namespace CommLibrary
{
    public class AngleRotateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (
                    value == null ||
                    (!(value is double numb) && !double.TryParse(value.ToString(), out numb))
                )
                return null;

            if (parameter != null && parameter.ToString().ToUpper().Contains("ABS"))
                return Math.Abs(numb);

            if (parameter != null && parameter.ToString().ToUpper().Contains("SIGN"))
                return numb >= 0 ? 1 : -1;
            return numb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

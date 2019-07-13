using System;
using System.Globalization;
using System.Windows.Data;

namespace CommLibrary
{
    /// <summary>Класс WPF конвертора для присвоения одного из двух значений</summary>
    public class IIfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool _val))
                if (parameter is Array _par && _par.Length >1)
                {
                    string _ret = _val ? _par.GetValue(0).ToString() : _par.GetValue(1).ToString();
                    return _ret;
                }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

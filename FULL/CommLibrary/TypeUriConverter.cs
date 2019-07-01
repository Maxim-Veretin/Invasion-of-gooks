using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CommLibrary
{
    /// <summary>Конвертер содержит словарь для выбора по заданым типам значений Uri </summary>
    [ValueConversion(typeof(Type), typeof(Uri))]
    public class TypeUriConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is IEnumerable<TypeUriPair> collection)) return null;
            Type type = value is Type ? (Type)value : value.GetType();
            return collection.FirstOrDefault(pair => pair.Key == type)?.Source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is IEnumerable<TypeUriPair> collection)) return null;
            string originalString;
            if (value is Uri uri)
                originalString = uri.OriginalString;
            else
                originalString = value.ToString();
            return collection.FirstOrDefault(pair => pair.Source.OriginalString == originalString).Key;
        }
    }

    public class TypeUriPair
    {
        public Type Key { get; set; }
        public Uri Source { get; set; }

        public TypeUriPair() { }
        public TypeUriPair(Type key, Uri value)
        {
            Key = key;
            Source = value;
        }
    }

}

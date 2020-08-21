using System;
using System.Globalization;
using System.Windows.Data;

namespace CSharpSample03
{
    public class RotationValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double textValue = (double)value;
            return String.Format("{0:000}度：", textValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

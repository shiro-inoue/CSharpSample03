using System;
using System.Globalization;
using System.Windows.Data;

namespace CSharpSample03
{
    public class ThumbnailWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Format("{0}", (double)value * 40);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

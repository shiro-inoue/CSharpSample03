using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace CSharpSample03
{
    public class ThumbnailSizeConverter : IValueConverter
    {
        Dictionary<int, string> colors = new Dictionary<int, string>()
        { {1, "Small "},
          {2, "Middle"},
          {3, "Large "}
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Format("{0}", colors[(int)(double)value]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

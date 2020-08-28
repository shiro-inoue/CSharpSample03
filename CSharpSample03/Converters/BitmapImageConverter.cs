using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CSharpSample03
{
    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
#if false
                return new BitmapImage(new Uri(value.ToString()));
#else
                var bmpImage = new BitmapImage();
                var stream = File.OpenRead(value.ToString());
                bmpImage.BeginInit();
                bmpImage.DecodePixelWidth = 80;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.StreamSource = stream;
                bmpImage.EndInit();
                stream.Close();
                return bmpImage;
#endif
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

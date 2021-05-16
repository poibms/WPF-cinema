using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace WPF_cinema.Assistants.Converters
{
    class ToImg : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + $@"\PalaceOfArts\films\{(int)value}\cover.jpg"))
            {
                BitmapImage imgTemp = new BitmapImage();
                imgTemp.BeginInit();
                imgTemp.CacheOption = BitmapCacheOption.OnLoad;
                imgTemp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                imgTemp.UriSource = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + $@"\PalaceOfArts\films\{(int)value}\cover.jpg");
                imgTemp.EndInit();
                return imgTemp;
            }
            else
            {
                return "/Styles/img/unnamed.png";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter,
           System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

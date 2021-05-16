using System;
using System.Windows.Data;

namespace WPF_cinema.Assistants.Converters
{
    class HallsIdToHallsNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            using (CinemaDBContext context = new CinemaDBContext())
            {
                return context.Sessions.Find((int)value).Halls;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter,
           System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

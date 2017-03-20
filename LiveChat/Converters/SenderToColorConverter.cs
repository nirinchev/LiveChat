using System;
using System.Globalization;
using Xamarin.Forms;

namespace LiveChat
{
    public class SenderToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            if (stringValue == UserDetails.Current.Username)
            {
                return Color.Fuchsia;
            }

            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

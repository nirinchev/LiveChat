using System;
using System.Globalization;
using Xamarin.Forms;

namespace LiveChat
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool) || (bool)value)
            {
                return Color.White;
            }

            return Color.FromRgb(114, 216, 118);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

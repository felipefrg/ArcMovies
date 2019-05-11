using System;
using System.Globalization;
using Xamarin.Forms;

namespace ArcMovies.Converter
{
    public class BoolToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength result = new GridLength(0);
            if (value is bool isLoading)
            {
                if (isLoading)
                {
                    if (parameter != null && parameter is GridLength gridLength)
                    {
                        result = gridLength;
                    }
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}

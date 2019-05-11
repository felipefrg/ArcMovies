using System;
using System.Globalization;
using Xamarin.Forms;

namespace ArcMovies.Converter
{
    public class DivideNonNegativeNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            if (value is double newValue)
            {
                double numerator = newValue < 0 ? 0 : newValue;

                double divideBy = 1;
                if (double.TryParse(parameter.ToString(), out double denominator))
                {
                    divideBy = denominator > 1 ? denominator : 1;
                }

                result = numerator / divideBy;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

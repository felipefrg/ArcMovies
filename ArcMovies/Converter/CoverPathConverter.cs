using System;
using System.Globalization;
using System.Text.RegularExpressions;
using ArcMovies.Helper;

namespace ArcMovies.Converter
{
    public class CoverPathConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if(value != null && value is string path)
            {
                string imageWidth = "w185";
                if(parameter != null && parameter is string width)
                {
                    if(Regex.Match(width, "w\\d{1,3}").Success)
                    {
                        imageWidth = width;
                    }
                }

                result =  AppConst.TMDbRootImageURL + $"/{imageWidth}/" + path; 
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

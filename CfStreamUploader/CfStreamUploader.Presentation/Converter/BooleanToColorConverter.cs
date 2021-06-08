using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace CfStreamUploader.Presentation.Converter
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "Green";
            }
            return "#6497e8";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color && (Color)value == Color.LightSkyBlue)
            {
                return true;
            }
            return false;
        }
    }
}

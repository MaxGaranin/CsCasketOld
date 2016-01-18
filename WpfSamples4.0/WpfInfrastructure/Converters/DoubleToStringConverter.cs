using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WpfSamples40.Utils;

namespace WpfSamples40.WpfInfrastructure.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double) value;
            return doubleValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException();

            var s = value.ToString().DecSepToSys();
            double doubleValue;
            if (double.TryParse(s, out doubleValue))
                return doubleValue;
            
            return DependencyProperty.UnsetValue;
        }
    }
}
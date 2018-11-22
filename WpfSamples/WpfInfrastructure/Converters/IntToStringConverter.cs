using System;
using System.Globalization;
using System.Windows.Data;
using WpfSamples.Utils;

namespace WpfSamples.WpfInfrastructure.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as string;
            input = input.DecSepToSys();

            int result;
            var status = int.TryParse(input, out result);
            if (!status)
            {
                throw new ArgumentException("Невозможно преобразовать строку в число");
            }

            return result;
        }
    }
}
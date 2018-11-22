using System.Globalization;
using System.Windows.Controls;
using WpfSamples.Utils;

namespace WpfSamples.WpfInfrastructure.ValidationRules
{
    public class DoubleRangeValidationRule : ValidationRule
    {
        public double? Min { get; set; }
        public double? Max { get; set; }

        public DoubleRangeValidationRule()
        {
        }

        public DoubleRangeValidationRule(double? min = null, double? max = null)
        {
            Min = min;
            Max = max;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = value as string;
            input = input.DecSepToSys();

            double result;
            var status = double.TryParse(input, out result);
            if (!status)
            {
                return new ValidationResult(false, "Значение должно быть числовым");
            }

            var minCondition = true;
            if (Min != null) minCondition = result >= Min;

            var maxCondition = true;
            if (Max != null) maxCondition = result <= Max;

            if (!(minCondition && maxCondition))
            {
                var message = string.Empty;

                if ((Min != null) && (Max != null))
                {
                    message = string.Format("Значение должно быть в диапазоне [{0}:{1}]", Min, Max);
                }
                if ((Min != null) && (Max == null))
                {
                    message = string.Format("Значение должно быть больше или равно {0}", Min);
                }
                if ((Min == null) && (Max != null))
                {
                    message = string.Format("Значение должно быть меньше или равно {0}", Max);
                }

                return new ValidationResult(false, message);
            }

            return ValidationResult.ValidResult;
        }
    }
}
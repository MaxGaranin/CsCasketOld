using System.Globalization;
using System.Windows.Controls;
using WpfSamples.WpfInfrastructure.Classes;

namespace WpfSamples.WpfInfrastructure.ValidationRules
{
    public class MyStringLengthValidationRule : ValidationRule
    {
        public IntegerContainer LengthContainer { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length <= LengthContainer.Value)
                return new ValidationResult(true, null);

            return new ValidationResult(false, "The string is to long");
        }
    }
}
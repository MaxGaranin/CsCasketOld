using System.Windows;
using WpfSamples40.WpfInfrastructure.ValidationRules;

namespace WpfSamples40.View
{
    /// <summary>
    /// Interaction logic for ValidationTestView.xaml
    /// </summary>
    public partial class ValidationTestView : Window
    {
        public ValidationTestView()
        {
            InitializeComponent();

            var b = ControlTextBox.GetMyValueBinding;
            var rule = new DoubleRangeValidationRule { Min = 0, Max = 100 };
            b.ValidationRules.Add(rule);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
//            // Не работает
//            var binding = BindingOperations.GetBinding(controlTextBox, TextBoxControl.MyNameProperty);
//            var rule = new DoubleRangeValidationRule { Min = 0, Max = 100 };
//            binding.ValidationRules.Add(rule);

        }
    }
}

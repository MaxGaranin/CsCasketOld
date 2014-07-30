using System.Windows;

namespace WpfSamples40.ComplexSamples.FluidModels.Views
{
    /// <summary>
    /// Interaction logic for FluidModelBankView.xaml
    /// </summary>
    public partial class FluidModelBankView : Window
    {
        public FluidModelBankView()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

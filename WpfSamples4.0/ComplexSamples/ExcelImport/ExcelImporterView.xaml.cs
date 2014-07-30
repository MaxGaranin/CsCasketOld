using System.Windows;

namespace WpfSamples40.ComplexSamples.ExcelImport
{
    public partial class ExcelImporterView : Window
    {
        public ExcelImporterView()
        {
            InitializeComponent();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

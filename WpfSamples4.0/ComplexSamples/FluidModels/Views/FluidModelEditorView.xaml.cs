using System.Windows;

namespace WpfSamples40.ComplexSamples.FluidModels.Views
{
    /// <summary>
    /// Interaction logic for FluidModelEditorView.xaml
    /// </summary>
    public partial class FluidModelEditorView : Window
    {
        public FluidModelEditorView()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

using System.Windows;

namespace WpfSamples.View.MasterWizard
{
    public partial class MasterView : Window
    {
        public MasterView()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System.Windows;

namespace WpfSamples.View
{
    public partial class NotifyIconView : Window
    {
        public NotifyIconView()
        {
            InitializeComponent();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
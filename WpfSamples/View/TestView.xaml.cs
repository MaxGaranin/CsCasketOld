using System.Windows;

namespace WpfSamples.View
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        public TestView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            StringTextBox.Text = "ddddddddddddd";
        }
    }
}

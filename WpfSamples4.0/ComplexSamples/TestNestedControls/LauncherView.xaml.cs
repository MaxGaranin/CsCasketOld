using System.Windows;

namespace WpfSamples40.ComplexSamples.TestNestedControls
{
    /// <summary>
    /// Interaction logic for LauncherView.xaml
    /// </summary>
    public partial class LauncherView : Window
    {
        public LauncherView()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var launcher = new NestedControlsLauncher();
            launcher.Run();
        }
    }
}

using System.Windows;
using WpfSamples45.View;

namespace WpfSamples45
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var view = new MainWindow();
            view.Show();
        }
    }
}

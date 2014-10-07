using System.Windows;
using MvvmTest40.View;

namespace MvvmTest40
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

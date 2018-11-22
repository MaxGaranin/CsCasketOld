using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
using WpfSamples.View;
using WpfSamples.ViewModel;
using WpfSamples.WpfInfrastructure.Utils;

namespace WpfSamples
{
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            Init();

            // var view = new TaskWithDialogView();

            var viewModel = new ValidationTestViewModel();
            var view = new ValidationTestView { DataContext = viewModel };
            view.Show();
        }

        private void Init()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();

            // Применяет культуру системы к WPF
            LocalePatch.Init();
        }
    }
}
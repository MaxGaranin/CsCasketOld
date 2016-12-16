using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
using WpfSamples40.View;
using WpfSamples40.ViewModel;
using WpfSamples40.ViewModel.Async;
using WpfSamples40.WpfInfrastructure.Utils;
using ProgressBarTestView = WpfSamples40.View.Async.ProgressBarTestView;

namespace WpfSamples40
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

            var viewModel = new ProgressBarTestViewModel();
            var view = new ProgressBarTestView {DataContext = viewModel};
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
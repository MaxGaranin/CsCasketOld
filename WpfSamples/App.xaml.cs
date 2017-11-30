using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
using WpfSamples40.View;
using WpfSamples40.View.Async.TaskWithDialog;
using WpfSamples40.ViewModel;
using WpfSamples40.WpfInfrastructure.Utils;

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
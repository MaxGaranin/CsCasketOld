using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
using WpfSamples40.Utils;
using WpfSamples40.View;
using WpfSamples40.ViewModel;

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

            var viewModel = new ValidationTestViewModel();
            var view = new ValidationTestView { DataContext = viewModel };
            view.Show();

//            var viewModel = new ProgressBarTestViewModel();
//            var view = new ProgressBarTestView {DataContext = viewModel};
//            view.Show();
        }

        private void Init()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            
            // Применяет культуру системы к WPF
            LocalePatch.Init();
        }
    }
}
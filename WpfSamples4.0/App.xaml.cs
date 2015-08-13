using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
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

            var view = new AnimationTestView();
            view.Show();

//            var viewModel = new ProgressBarTestViewModel();
//            var view = new ProgressBarTestView {DataContext = viewModel};
//            view.Show();
        }

        private void Init()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();

            // Важная штука: применяет культуру системы к WPF.
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof (FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}
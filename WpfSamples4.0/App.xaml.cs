using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
using WpfSamples40.ComplexSamples.FluidModels;
using WpfSamples40.ComplexSamples.GraphEditor;
using WpfSamples40.ComplexSamples.Measures;
using WpfSamples40.View;

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

//            var view = new TestMeasureView();
//            var view = new MeasureWithControlView();
//            var view = new GraphEditorView();

//            var view = new TabView();
//            view.Show();

//            var launcher = new FluidModelsBankLauncher();
//            launcher.Run();

            var view = new ShowImageView();
            view.Show();
        }

        private void Init()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();

            // Важная штука: применяет культуру системы к WPF.
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));             
        }
    }
}

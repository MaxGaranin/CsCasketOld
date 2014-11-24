using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Threading;
using Telerik.Windows.Controls;
using WpfSamples40.View;
using WpfSamples40.View.Master;
using WpfSamples40.ViewModel.Master;

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

            OpenMasterView();
        }

        private void OpenMasterView()
        {
            var viewModel = new MasterViewModel();
            viewModel.TabViewModels = new ObservableCollection<ITabViewModel>
            {
                new Control1ViewModel
                {
                    Header = "Control1",
                    Text1 = "Text1",
                    TestValue = 25
                },
                new Control2ViewModel
                {
                    Header = "Control2",
                    Text2 = "Text2"
                },
                new Control3ViewModel
                {
                    Header = "Control3",
                    Text3 = "Text3"
                },
            };

            var view = new MasterView();
            view.DataContext = viewModel;
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

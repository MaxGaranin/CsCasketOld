using System.Collections.ObjectModel;
using WpfSamples40.View.MasterWizard;

namespace WpfSamples40.ViewModel.MasterWizard
{
    public class MasterViewHelper
    {
        public static void OpenMasterView()
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
    }
}
using System.Windows.Controls;
using WpfSamples.ViewModel.MasterWizard;

namespace WpfSamples.View.MasterWizard
{
    /// <summary>
    /// Interaction logic for Control2.xaml
    /// </summary>
    public partial class Control2 : UserControl, ITabViewModel
    {
        public Control2()
        {
            InitializeComponent();
        }

        public string Header { get; set; }
    }
}

using System.Windows.Controls;
using WpfSamples40.ViewModel.MasterWizard;

namespace WpfSamples40.View.MasterWizard
{
    /// <summary>
    /// Interaction logic for Control3.xaml
    /// </summary>
    public partial class Control3 : UserControl, ITabViewModel
    {
        public Control3()
        {
            InitializeComponent();
        }

        public string Header { get; set; }
    }
}

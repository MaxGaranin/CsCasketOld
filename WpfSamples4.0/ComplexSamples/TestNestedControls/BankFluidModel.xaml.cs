using System.Windows;
using System.Windows.Controls;

namespace WpfSamples40.ComplexSamples.TestNestedControls
{
    /// <summary>
    /// Interaction logic for BankFluidModel.xaml
    /// </summary>
    public partial class BankFluidModel : UserControl
    {
        public BankFluidModel()
        {
            InitializeComponent();
        }

        public UIElement ButtonsPlace
        {
            get { return (UIElement)GetValue(ButtonsPlaceProperty); }
            set { SetValue(ButtonsPlaceProperty, value); }
        }

        public static readonly DependencyProperty ButtonsPlaceProperty =
            DependencyProperty.Register("ButtonsPlace", typeof(UIElement), typeof(BankFluidModel), new PropertyMetadata(null));
    }
}

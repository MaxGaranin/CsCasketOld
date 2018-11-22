using System.Windows;
using System.Windows.Controls;

namespace WpfSamples.View
{
    public partial class ValidationTestControl : UserControl
    {
        public ValidationTestControl()
        {
            InitializeComponent();

            DataContextChanged += (sender, args) =>
            {
                var elem = this.Resources["DataContextBridge"] as FrameworkElement;
                elem.DataContext = args.NewValue;
            };
        }
    }
}
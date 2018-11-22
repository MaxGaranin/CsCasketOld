using System.Windows;

namespace WpfSamples.View.ControlBindings
{
    public partial class RepeatView : Window
    {
        public RepeatView()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;

            Value = "Hello, world";
        }

        public string Value
        {
            get { return (string) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (string), typeof (RepeatView), new PropertyMetadata(null));
    }
}
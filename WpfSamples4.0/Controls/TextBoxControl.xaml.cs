using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfSamples40.Controls
{
    public partial class TextBoxControl : UserControl
    {
        public TextBoxControl()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }

        public Binding GetMyValueBinding
        {
            get { return BindingOperations.GetBinding(txtMyValue, TextBox.TextProperty); }
        }

        public string MyName
        {
            get { return (string) GetValue(MyNameProperty); }
            set { SetValue(MyNameProperty, value); }
        }

        public static readonly DependencyProperty MyNameProperty =
            DependencyProperty.Register("MyName", typeof (string), typeof (TextBoxControl), new PropertyMetadata(null));

        public int MyIntValue
        {
            get { return (int) GetValue(MyIntValueProperty); }
            set { SetValue(MyIntValueProperty, value); }
        }

        public static readonly DependencyProperty MyIntValueProperty =
            DependencyProperty.Register("MyIntValue", typeof (int), typeof (TextBoxControl), new PropertyMetadata(0));

        public double MyDoubleValue
        {
            get { return (double) GetValue(MyDoubleValueProperty); }
            set { SetValue(MyDoubleValueProperty, value); }
        }

        public static readonly DependencyProperty MyDoubleValueProperty =
            DependencyProperty.Register("MyDoubleValue", typeof (double), typeof (TextBoxControl), new PropertyMetadata(0));
    }
}
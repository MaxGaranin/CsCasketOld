using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfSamples40.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxControl.xaml
    /// </summary>
    public partial class TextBoxControl : UserControl
    {
        public TextBoxControl()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }

        public Binding GetMyValueBinding {
            get { return BindingOperations.GetBinding(txtMyValue, TextBox.TextProperty); } 
        }

        public string MyName
        {
            get { return (string)GetValue(MyNameProperty); }
            set { SetValue(MyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyNameProperty =
            DependencyProperty.Register("MyName", typeof(string), typeof(TextBoxControl), new PropertyMetadata(null));




        public int MyValue
        {
            get { return (int)GetValue(MyValueProperty); }
            set { SetValue(MyValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyValueProperty =
            DependencyProperty.Register("MyValue", typeof(int), typeof(TextBoxControl), new PropertyMetadata(null));

        
    }
}

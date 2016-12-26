using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfSamples40.View.ControlBindings
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
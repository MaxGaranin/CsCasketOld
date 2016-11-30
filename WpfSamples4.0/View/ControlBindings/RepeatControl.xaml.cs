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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSamples40.View.ControlBindings
{
    public partial class RepeatControl : UserControl
    {
        public RepeatControl()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public string Repeat1
        {
            get { return (string) GetValue(Repeat1Property); }
            set { SetValue(Repeat1Property, value); }
        }

        public static readonly DependencyProperty Repeat1Property =
            DependencyProperty.Register("Repeat1", typeof (string), typeof (RepeatControl),
                new PropertyMetadata(null, (s, e) =>
                {
                    var c = s as RepeatControl;
                    c.Repeat2 = (string) e.NewValue;
                }));

        public string Repeat2
        {
            get { return (string) GetValue(Repeat2Property); }
            set { SetValue(Repeat2Property, value); }
        }

        public static readonly DependencyProperty Repeat2Property =
            DependencyProperty.Register("Repeat2", typeof (string), typeof (RepeatControl),
                new PropertyMetadata(null, (s, e) =>
                {
                    var c = s as RepeatControl;
                    c.Repeat1 = (string) e.NewValue;
                }));
    }
}
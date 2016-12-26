using System.Windows;
using System.Windows.Controls;

namespace WpfSamples40.View.ControlBindings
{
    public partial class RepeatControl : UserControl
    {
        private bool _isSetInside;

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
                new FrameworkPropertyMetadata(null, 
                (s, e) =>
                {
//                    var c = s as RepeatControl;
//                    c.Repeat2 = (string) e.NewValue;
                },
                (s, value) =>
                {
                    var c = s as RepeatControl;
                    if (c._isSetInside)
                    {
                        return c.Repeat2;
                    }
                    else
                    {
                        return value;
                    }
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

                    c._isSetInside = true;
                    s.CoerceValue(Repeat1Property);
                    c._isSetInside = false;
                }));
    }
}
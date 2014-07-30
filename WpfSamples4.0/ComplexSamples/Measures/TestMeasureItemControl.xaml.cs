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

namespace WpfSamples40.ComplexSamples.Measures
{
    public partial class TestMeasureItemControl : UserControl
    {
        public TestMeasureItemControl()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }

        public MeasureContainer TempMeasureContainer
        {
            get { return (MeasureContainer)GetValue(TempMeasureContainerProperty); }
            set { SetValue(TempMeasureContainerProperty, value); }
        }

        public static readonly DependencyProperty TempMeasureContainerProperty =
            DependencyProperty.Register("TempMeasureContainer", typeof(MeasureContainer), typeof(TestMeasureItemControl),
            new PropertyMetadata(null, (s, e) =>
            {
            }));

    }
}

using System.Windows;
using System.Windows.Controls;

namespace WpfSamples40.View.ContentPresenterVsContentControl
{
    public partial class DualContentControl : UserControl
    {
        public DualContentControl()
        {
            InitializeComponent();
            this.LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty ContentOneProperty = DependencyProperty.Register("ContentOne", typeof (object),
            typeof (DualContentControl));

        public static readonly DependencyProperty ContentOneTemplateProperty = DependencyProperty.Register("ContentOneTemplate",
            typeof (DataTemplate), typeof (DualContentControl));

        public static readonly DependencyProperty ContentTwoProperty = DependencyProperty.Register("ContentTwo", typeof (object),
            typeof (DualContentControl));

        public static readonly DependencyProperty ContentTwoTemplateProperty = DependencyProperty.Register("ContentTwoTemplate",
            typeof (DataTemplate), typeof (DualContentControl));

        public object ContentOne
        {
            get { return GetValue(ContentOneProperty); }
            set { SetValue(ContentOneProperty, value); }
        }

        public DataTemplate ContentOneTemplate
        {
            get { return (DataTemplate) GetValue(ContentOneTemplateProperty); }
            set { SetValue(ContentOneTemplateProperty, value); }
        }

        public object ContentTwo
        {
            get { return GetValue(ContentTwoProperty); }
            set { SetValue(ContentTwoProperty, value); }
        }

        public DataTemplate ContentTwoTemplate
        {
            get { return (DataTemplate) GetValue(ContentTwoTemplateProperty); }
            set { SetValue(ContentTwoTemplateProperty, value); }
        }
    }
}
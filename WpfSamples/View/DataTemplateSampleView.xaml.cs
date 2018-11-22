using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using WpfSamples.Properties;

namespace WpfSamples.View
{
    public partial class DataTemplateSampleView : Window, INotifyPropertyChanged
    {
        public DataTemplateSampleView()
        {
            InitializeComponent();

            Init();
            Value = 5;

            LayoutRoot.DataContext = this;
        }

        private void Init()
        {
            var template = CreateTemplate();
            Presenter.ContentTemplate = template;
        }

        private DataTemplate CreateTemplate()
        {
            const string xamlTemplate = "<DataTemplate x:Name=\"Tpl\">" +
                                        "   <TextBox Background=\"{{Binding Background, ElementName=Txt}}\"" +
                                        "            Text=\"{{Binding Value}}\" />" +
                                        "</DataTemplate>";
            var xaml = string.Format(xamlTemplate);

            var context = new ParserContext();

            var type = typeof(DataTemplateSampleView);
            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", type.Namespace, type.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("v", "v");

            var template = (DataTemplate) XamlReader.Parse(xaml, context);
            return template;
        }

        #region Value

        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public static class Info
    {
        public static string Kol = "info";
    }
}
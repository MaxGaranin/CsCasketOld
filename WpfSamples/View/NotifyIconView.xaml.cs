using System;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace WpfSamples.View
{
    public partial class NotifyIconView : Window
    {
        public NotifyIconView()
        {
            InitializeComponent();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NotifyIconView_OnLoaded(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlSettings))
            {
                XamlWriter.Save(TextBox.Template, xmlWriter);
            }

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
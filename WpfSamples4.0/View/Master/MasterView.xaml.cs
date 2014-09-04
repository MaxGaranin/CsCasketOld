using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace WpfSamples40.View.Master
{
    public partial class MasterView : Window
    {
        public MasterView()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

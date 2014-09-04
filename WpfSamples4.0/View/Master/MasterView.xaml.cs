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

//            MasterTabControl.SelectedIndex = 0;
//            UpdateButtons();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
//            MasterTabControl.SelectedIndex--;
//            UpdateButtons();
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
//            MasterTabControl.SelectedIndex++;
//            UpdateButtons();
        }

        private void UpdateButtons()
        {
//            BackButton.IsEnabled = true;
//            ForwardButton.IsEnabled = true;
//            OkButton.IsEnabled = false;
//
//            if (MasterTabControl.SelectedIndex == 0)
//            {
//                BackButton.IsEnabled = false;
//            }
//            else if (MasterTabControl.SelectedIndex == MasterTabControl.Items.Count - 1)
//            {
//                ForwardButton.IsEnabled = false;
//                OkButton.IsEnabled = true;
//            }
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

﻿using System.Windows;

namespace WpfSamples40.View.Async.ProgressBarSample
{
    public partial class ProgressBarView : Window
    {
        public ProgressBarView()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
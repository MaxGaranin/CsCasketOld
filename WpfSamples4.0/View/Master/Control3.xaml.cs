﻿using System;
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
using WpfSamples40.ViewModel.Master;

namespace WpfSamples40.View.Master
{
    /// <summary>
    /// Interaction logic for Control3.xaml
    /// </summary>
    public partial class Control3 : UserControl, ITabViewModel
    {
        public Control3()
        {
            InitializeComponent();
        }

        public string Header { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfSamples40.View
{
    public partial class CustomDataGridTestView : Window
    {
        public CustomDataGridTestView()
        {
            InitializeComponent();

            DataContext = this;

            Init();
        }

        private void Init()
        {
            SomeDataItems = new ObservableCollection<SomeDataItem>
            {
                new SomeDataItem {Name = "Test1", IntValue = 22, DoubleValue = 34.2},
                new SomeDataItem {Name = "Test2", IntValue = 56, DoubleValue = 122.2},
                new SomeDataItem {Name = "Test3", IntValue = 78, DoubleValue = 66.7},
            };
        }

        public ObservableCollection<SomeDataItem> SomeDataItems { get; set; }
    }

    public class SomeDataItem
    {
        public string Name { get; set; }
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
    }
}
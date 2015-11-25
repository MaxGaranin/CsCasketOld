using System;
using System.Collections.ObjectModel;
using System.Windows;

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
            var now = DateTime.Now.Date;

            SomeDataItems = new ObservableCollection<SomeDataItem>
            {
                new SomeDataItem {Name = "Test1", IntValue = 22, DoubleValue = 34.2, BeginDate = now, EndDate = now},
                new SomeDataItem {Name = "Test2", IntValue = 56, DoubleValue = 122.2, BeginDate = now, EndDate = now},
                new SomeDataItem {Name = "Test3", IntValue = 78, DoubleValue = 66.7, BeginDate = now, EndDate = now},
            };
        }

        public ObservableCollection<SomeDataItem> SomeDataItems { get; set; }
    }

    public class SomeDataItem
    {
        public string Name { get; set; }
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
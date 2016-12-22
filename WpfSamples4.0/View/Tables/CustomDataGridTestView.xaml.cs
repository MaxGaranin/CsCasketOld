using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfSamples40.View.Tables
{
    public partial class CustomDataGridTestView : Window
    {
        public CustomDataGridTestView()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
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

            RegionNames = new []{""}.Concat(new[] {"Самаранефтегаз", "Оренбургнефть", "Ванкор"}).ToArray();
        }

        public ObservableCollection<SomeDataItem> SomeDataItems { get; set; }

        public string[] RegionNames { get; set; }
    }

    public class SomeDataItem
    {
        public string Name { get; set; }
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string RegionName { get; set; }
    }
}
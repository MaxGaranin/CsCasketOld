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
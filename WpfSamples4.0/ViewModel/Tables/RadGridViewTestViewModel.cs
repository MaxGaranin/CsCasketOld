using System.Collections.ObjectModel;
using WpfSamples40.ViewModel.HelperClasses;

namespace WpfSamples40.ViewModel.Tables
{
    public class RadGridViewTestViewModel
    {
        public ObservableCollection<BigData> BigDatas { get; set; }

        public RadGridViewTestViewModel()
        {
            BigDatas = new ObservableCollection<BigData>();

            for (int i = 0; i < 100; i++)
            {
                BigDatas.Add(CreateBigDataItem(i));
            }
        }

        private BigData CreateBigDataItem(int i)
        {
            var data = new BigData();
            data.Data1 = "Item" + i++;
            data.Data2 = "Item" + i++;
            data.Data3 = "Item" + i++;
            data.Data4 = "Item" + i++;
            data.Data5 = "Item" + i++;
            data.Data6 = "Item" + i++;
            data.Data7 = "Item" + i++;
            data.Data8 = "Item" + i++;
            data.Data9 = "Item" + i++;
            data.Data10 = "Item" + i++;
            data.Data11 = "Item" + i++;
            data.Data12 = "Item" + i++;
            data.Data13 = "Item" + i++;
            data.Data14 = "Item" + i++;
            data.Data15 = "Item" + i++;
            data.Data16 = "Item" + i++;
            data.Data17 = "Item" + i++;
            data.Data18 = "Item" + i++;
            data.Data19 = "Item" + i++;
            data.Data20 = "Item" + i++;

            return data;
        }
    }
}
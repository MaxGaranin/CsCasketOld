using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using WpfSamples40.Properties;

namespace WpfSamples40.View.Tables
{
    public partial class DataTableAutoGenerateView : Window, INotifyPropertyChanged
    {
        private DataTable _myTable;

        public DataTableAutoGenerateView()
        {
            InitializeComponent();

            DataContext = this;

            MyTable = CreateDataTable();
        }

        public DataTable MyTable
        {
            get { return _myTable; }
            set
            {
                if (Equals(value, _myTable)) return;
                _myTable = value;
                RaisePropertyChanged("MyTable");
            }
        }


        private DataTable CreateDataTable()
        {
            var table = new DataTable();
            table.Columns.Add("Well", typeof(string));
            table.Columns.Add("Date", typeof(string));
            table.Columns.Add("Kr", typeof(double));
            table.Columns.Add("Pd", typeof(double));
            table.Columns.Add("H", typeof(double));

            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                DataRow newRow = table.NewRow();
                newRow["Well"] = "Скважина " + i;
                newRow["Date"] = DateTime.Now;
                newRow["Kr"] = i;
                newRow["Pd"] = i + 2;
                newRow["H"] = random.NextDouble()*100;
                table.Rows.Add(newRow);
            }

            return table;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

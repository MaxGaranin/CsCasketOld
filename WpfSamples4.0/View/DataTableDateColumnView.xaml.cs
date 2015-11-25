using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfSamples40.Annotations;

namespace WpfSamples40.View
{
    public partial class DataTableDateColumnView : Window, INotifyPropertyChanged
    {
        private const int DateIndex = 1;

        private DataTable _myTable;

        public DataTableDateColumnView()
        {
            InitializeComponent();
            DataContext = this;

            MyTable = CreateDataTable();
            HeaderPart = "[ru]";
        }

        public string HeaderPart { get; set; }

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

            var column = new DataColumn
            {
                ColumnName = "Date",
                DataType = typeof(DateTime),
                AllowDBNull = false,
                DefaultValue = DateTime.Now
            };
            table.Columns.Add(column);

            table.Columns.Add("Kr", typeof(double));
            table.Columns.Add("Pd", typeof(double));
            table.Columns.Add("H", typeof(double));
            table.Columns.Add("IsSelected", typeof(bool));

            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                DataRow newRow = table.NewRow();
                newRow["Well"] = "Скважина " + i;
                newRow["Date"] = DateTime.Now;
                newRow["Kr"] = i;
                newRow["Pd"] = i + 2;
                newRow["H"] = random.NextDouble()*100;
                newRow["IsSelected"] = false;
                table.Rows.Add(newRow);
            }

            return table;
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0) return;

            var currentCell = e.AddedCells[0];

            if (Equals(currentCell.Column, TestDataGrid.Columns[DateIndex]))
            {
                TestDataGrid.BeginEdit();
            }
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

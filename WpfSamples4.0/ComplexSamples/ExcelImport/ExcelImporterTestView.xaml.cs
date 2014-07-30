using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using WpfSamples40.Annotations;
using WpfSamples40.Properties;

namespace WpfSamples40.ComplexSamples.ExcelImport
{
    public partial class ExcelImporterTestView : Window, INotifyPropertyChanged
    {
        private DataTable _excelTable;

        public ExcelImporterTestView()
        {
            InitializeComponent();

            DataContext = this;

            Names = new List<string> {"Скважина", "Дата", "Добыча нефти", "Добыча воды"};
        }

        public DataTable ExcelTable
        {
            get { return _excelTable; }
            set
            {
                if (Equals(value, _excelTable)) return;
                _excelTable = value;
                RaisePropertyChanged();
            }
        }

        public IList<string> Names { get; set; }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            var excelImporter = new ExcelImporter();
            var result = excelImporter.Process(Names);
            if (!result) return;

            string[,] dataArray = excelImporter.DataArray;
            ExcelTable = ExcelImporterHelper.CreateDataTableFrom2DArray(dataArray, Names);

            string currentRowInfo = null;
            try
            {
                var levelItems = new List<ProductionLevelItem>();

                var rowsCount = dataArray.GetLength(0);
                var columnsCount = dataArray.GetLength(1);
                for (int i = 0; i < rowsCount; i++)
                {
                    currentRowInfo = CreateCurrentRowInfo(dataArray, columnsCount, i);

                    var well = dataArray[i, 0];
                    var date = DateTime.Parse(dataArray[i, 1]);
                    var qOil = double.Parse(dataArray[i, 2]);
                    var qWat = double.Parse(dataArray[i, 2]);
                    levelItems.Add(new ProductionLevelItem()
                    {
                        Well = well,
                        Date = date,
                        Qoil = qOil,
                        Qwat = qWat
                    });
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(string.Format("Ошибка при импорте: '{0}'\n\n Строка: {1}", exc.Message, currentRowInfo));
                return;
            }
        }

        private string CreateCurrentRowInfo(string[,] dataArray, int columnsCount, int currentRow)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < columnsCount; i++)
            {
                var s = dataArray[currentRow, i] ?? "{Пустое значение}";
                sb.Append(s + "|");
            }

            return sb.ToString();
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class ProductionLevelItem
    {
        public string Well { get; set; }

        public DateTime Date { get; set; }

        public double Qoil { get; set; }

        public double Qwat { get; set; }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfSamples40.Helpers;

namespace WpfSamples40.ComplexSamples.ExcelImport
{
    public class ExcelImporterViewModel : ViewModelBase
    {
        private const int TABLE_ROWS_LIMIT = 100;
        private const int FIRST_ROW = 1;

        private IList<string> _names;

        private DataTable _excelTable;
        private string _fileName;
        private IList<ExcelSheet> _sheets;
        private ExcelSheet _currentSheet;
        private int _firstRow;

        private IList<string> _columnNames;
        private IList<NameIndex> _nameIndices;
        private IList<ColumnContainer> _columnContainers;
            
        public ExcelImporterViewModel(IEnumerable<string> names)
        {
            _names = names.ToList();
            FirstRow = FIRST_ROW;
        }

        public bool DialogResult { get; private set; }

        public string[,] ResultArray { get; private set; }

        public DataTable ExcelTable
        {
            get { return _excelTable; }
            set
            {
                if (Equals(value, _excelTable)) return;
                _excelTable = value;
                RaisePropertyChanged("ExcelTable");
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (Equals(value, _fileName)) return;
                _fileName = value;
                RaisePropertyChanged("FileName");
            }
        }

        public IList<ExcelSheet> Sheets
        {
            get { return _sheets; }
            set
            {
                if (Equals(value, _sheets)) return;
                _sheets = value;
                RaisePropertyChanged("Sheets");
            }
        }

        public ExcelSheet CurrentSheet
        {
            get { return _currentSheet; }
            set
            {
                if (Equals(value, _currentSheet)) return;
                _currentSheet = value;
                RaisePropertyChanged("CurrentSheet");

                if (value == null) return;

                OnCurrentSheetChanged(value);
            }
        }

        private void OnCurrentSheetChanged(ExcelSheet sheet)
        {
            var columnsCount = sheet.StrArray.GetLength(1);
            _columnNames = ExcelImporterHelper.GetColumnNames(columnsCount);
            ExcelTable = ExcelImporterHelper.CreateDataTableFrom2DArray(
                sheet.StrArray, _columnNames, TABLE_ROWS_LIMIT);

            FillColumnContainers(_columnNames);
            FillNameIndices(_names);
        }

        public int FirstRow
        {
            get { return _firstRow; }
            set
            {
                if (Equals(value, _firstRow)) return;
                _firstRow = value;
                RaisePropertyChanged("FirstRow");
            }
        }

        public IList<NameIndex> NameIndices
        {
            get { return _nameIndices; }
            set
            {
                if (Equals(value, _nameIndices)) return;
                _nameIndices = value;
                RaisePropertyChanged("NameIndices");
            }
        }

        public IList<ColumnContainer> ColumnContainers
        {
            get { return _columnContainers; }
            set
            {
                if (Equals(value, _columnContainers)) return;
                _columnContainers = value;
                RaisePropertyChanged("ColumnContainers");
            }
        }


        #region OpenExcelFile

		private RelayCommand _openExcelFileCommand;
        public ICommand OpenExcelFileCommand
        {
            get
            {
                return _openExcelFileCommand
                       ?? (_openExcelFileCommand = new RelayCommand(OpenExcelFile));
            }
        }

        private void OpenExcelFile()
        {
            var fileName = FileDialogHelper.GetOpenFileName("Открыть файл...",
                                                            "Файлы Microsoft Excel|*.xls;*.xlsx|Все файлы (*.*)|*.*");
            if (fileName == null) return;

            FileName = fileName;
            Sheets = ExcelImporterHelper.ReadExcelData(fileName);

            Debug.Assert(Sheets.Count > 0);
            CurrentSheet = Sheets[0];
        }

	    #endregion    

        #region Import

        private RelayCommand _importCommand;
        public ICommand ImportCommand
        {
            get
            {
                return _importCommand
                       ?? (_importCommand = new RelayCommand(Import));
            }
        }

        private void Import()
        {
            //TODO: Здесь должна быть проверка введенных данных
            // 1. Первый ряд не должен превышать общее число рядов
            // 2. Все соответствия должны быть заполнены

            var columnIndices = NameIndices.Select(n => n.ColumnIndex.Value).ToArray();
            var beginRowIndex = FirstRow - 1;
            ResultArray = ExcelImporterHelper.ExtractDataForUserSelection(CurrentSheet, columnIndices, beginRowIndex);

            DialogResult = true;
        } 

        #endregion

        #region Matching names and columns

        private void FillColumnContainers(IList<string> columnNames)
        {
            ColumnContainers = new List<ColumnContainer>(columnNames.Count);
            for (int i = 0; i < columnNames.Count; i++)
            {
                ColumnContainers.Add(new ColumnContainer
                {
                    ColumnIndex = i,
                    ColumnName = columnNames[i]
                });
            }
        }

        private void FillNameIndices(IEnumerable<string> names)
        {
            NameIndices = names
                .Select(n => new NameIndex() {Name = n, ColumnIndex = null})
                .ToList();
        }


        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace WpfSamples40.ComplexSamples.ExcelImport
{
    public class ExcelImporterHelper
    {
        private const string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LAST_COLUMN = "IV";

        public static string[] Columns
        {
            get
            {
                var columns = new List<string>();

                for (int i = 0; i < ABC.Length; i++)
                    columns.Add(ABC[i].ToString());

                for (int i = 0; i < ABC.Length; i++)
                    for (int j = 0; j < ABC.Length; j++)
                    {
                        string s = ABC[i].ToString() + ABC[j].ToString();
                        columns.Add(s);
                        if (s == LAST_COLUMN) break;
                    }

                return columns.ToArray();
            }
        }

        public static IList<string> GetColumnNames(int columnsCount)
        {
            return Columns.Take(columnsCount).ToList();
        }

        public static IList<ExcelSheet> ReadExcelData(string fileName)
        {
            Application excelApp = null;
            Workbook workbook = null;

            var sheets = new List<ExcelSheet>();

            try
            {
                excelApp = new Application();
                workbook = excelApp.Workbooks.Open(fileName);

                foreach (Worksheet worksheet in workbook.Sheets)
                {
                    Range usedRange = worksheet.UsedRange;

                    // Обработка ситуации, когда данные находятся не в начале листа
                    var lastColumn = usedRange.Columns.Count + usedRange.Column - 1;
                    var lastRow = usedRange.Rows.Count + usedRange.Row - 1;
                    usedRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[lastRow, lastColumn]];

                    var arrayRange = (object[,])usedRange.Value;

                    var sheet = new ExcelSheet() { Name = worksheet.Name };

                    sheet.StrArray = (arrayRange == null)
                        ? new string[0, 0]
                        : ExcelObjectArrayToString(arrayRange);

                    sheets.Add(sheet);
                }

            }
            finally
            {
                if (workbook != null) { workbook.Close(); Marshal.ReleaseComObject(workbook); }
                if (excelApp != null) { excelApp.Quit(); Marshal.ReleaseComObject(excelApp); }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            return sheets;
        }

        private static string[,] ExcelObjectArrayToString(object[,] dataArray)
        {
            int rows = dataArray.GetLength(0);
            int columns = dataArray.GetLength(1);

            // Массив данных из Excel начинается с 1
            var strArray = new string[rows - 1, columns - 1];

            for (int row = 0; row < rows - 1; row++)
            {
                for (int column = 0; column < columns - 1; column++)
                {
                    object obj = dataArray[row + 1, column + 1];
                    strArray[row, column] = (obj != null) ? obj.ToString() : null;
                }
            }

            return strArray;
        }

        public static string[,] ExtractDataForUserSelection(ExcelSheet sheet, int[] columnIndices, int startRowIndex)
        {
            int rowCount = sheet.StrArray.GetLength(0);
            int columnCount = sheet.StrArray.GetLength(1);

            Debug.Assert(columnCount >= columnIndices.Length);
            Debug.Assert(startRowIndex >= 0);
            Debug.Assert(startRowIndex < rowCount);

            var resultRows = rowCount - startRowIndex;
            var resultArray = new string[resultRows, columnIndices.Length];

            for (int i = startRowIndex; i < rowCount; i++)
            {
                for (int j = 0; j < columnIndices.Length; j++)
                {
                    resultArray[i - startRowIndex, j] = sheet.StrArray[i, columnIndices[j]];
                }
            }

            return resultArray;
        }

        public static DataTable CreateDataTableFrom2DArray(string[,] strArray, IEnumerable<string> columnNames, int rowsLimit = -1)
        {
            var rowsCount = strArray.GetLength(0);
            var columnsCount = strArray.GetLength(1);

            var table = CreateDataTable(columnNames, true);

            var useRowLimit = (rowsLimit > 0) && (rowsLimit < rowsCount);
            if (useRowLimit) rowsCount = rowsLimit;

            for (int i = 0; i < rowsCount; i++)
            {
                var row = table.NewRow();
                row[0] = i + 1;                         // Номера рядов
                for (int j = 0; j < columnsCount; j++)
                {
                    row[j + 1] = strArray[i, j];
                }
                table.Rows.Add(row);
            }

            if (useRowLimit) AddEllipsisRow(table);
            
            return table;
        }

        private static void AddEllipsisRow(DataTable table)
        {
            var row = table.NewRow();
            for (int j = 0; j < table.Columns.Count; j++)
            {
                row[j] = "...";
            }
            table.Rows.Add(row);
        }

        private static DataTable CreateDataTable(IEnumerable<string> columnNames, bool isCreateRowsNumberColumn = false)
        {
            var table = new DataTable();

            // Столбец с номерами рядов
            if (isCreateRowsNumberColumn)
            {
                table.Columns.Add("№", typeof(string));    
            }
            
            foreach (var columnName in columnNames)
            {
                table.Columns.Add(columnName, typeof(string));
            }

            return table;
        } 
    }
}
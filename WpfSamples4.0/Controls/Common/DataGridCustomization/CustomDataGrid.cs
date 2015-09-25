using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using Telerik.Windows.Controls;
using WpfSamples40.Utils;

namespace WpfSamples40.Controls.Common.DataGridCustomization
{
    public class CustomDataGrid : DataGrid
    {
        #region Constructor

        static CustomDataGrid()
        {
            CommandManager.RegisterClassCommandBinding(
                typeof (CustomDataGrid),
                new CommandBinding(ApplicationCommands.Paste, OnExecutedPaste, OnCanExecutePaste));
        }

        public CustomDataGrid()
        {
            //Стиль
//            Resources.MergedDictionaries.Add(new ResourceDictionary
//            {
//                Source = new Uri(@"pack://application:,,,/IPA;component/Resources/CustomDataGridTheme.xaml", UriKind.Absolute)
//            });

            MouseRightButtonDown += OnMouseRightButtonDown;
        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var menu = new RadContextMenu();

            var exportItem = new RadMenuItem {Header = "Выгрузить в Excel"};
            exportItem.Click += (s, e) => ExportToExcel();
            menu.Items.Add(exportItem);

            menu.HorizontalOffset = System.Windows.Forms.Cursor.Position.X;
            menu.VerticalOffset = System.Windows.Forms.Cursor.Position.Y;

            menu.IsOpen = true;
            mouseButtonEventArgs.Handled = true;
        }

        #endregion

        #region ExportToExcel

        private void ExportToExcel()
        {
            var array = new object[Items.Count + 1, Columns.Count];
            if (array.Length == 0) return;

            var i = 0;
            foreach (var col in Columns)
            {
                array[0, i++] = col.Header.ToString();
            }
            var r = 1;
            foreach (var item in Items)
            {
                var c = 0;
                foreach (var col in Columns)
                {
                    var val = col.OnCopyingCellClipboardContent(item);

                    if (!(val == null || val is string || val is double || val is int || val is DateTime))
                    {
                        Debug.WriteLine(val.GetType());
                        val = val.ToString();
                    }
                    array[r, c++] = val;
                }
                r++;
            }

            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Add();
            var xlWorksheet = (Worksheet) xlWorkbook.Worksheets.Item[1];

            Range c1 = xlWorksheet.Cells[1, 1];
            Range c2 = xlWorksheet.Cells[array.GetLength(0), array.GetLength(1)];
            Range range = xlWorksheet.Range[c1, c2];
            range.Value = array;

            xlWorksheet.Columns.AutoFit();
            var headersRange = xlWorksheet.Range["1:1"];
            headersRange.Font.Bold = true;
            headersRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            xlApp.Visible = true;
        }

        #endregion

        #region Clipboard Paste

        public static readonly DependencyProperty IsEnablePasteProperty =
            DependencyProperty.Register("IsEnablePaste", typeof (bool), typeof (CustomDataGrid),
                new PropertyMetadata(true));

        public bool IsEnablePaste
        {
            get { return (bool) GetValue(IsEnablePasteProperty); }
            set { SetValue(IsEnablePasteProperty, value); }
        }

        private static void OnCanExecutePaste(object target, CanExecuteRoutedEventArgs args)
        {
            ((CustomDataGrid) target).OnCanExecutePaste(args);
        }

        /// <summary>
        ///     This virtual method is called when ApplicationCommands.Paste command query its state.
        /// </summary>
        protected virtual void OnCanExecutePaste(CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = IsEnablePaste;
            args.Handled = IsEnablePaste;
        }

        private static void OnExecutedPaste(object target, ExecutedRoutedEventArgs args)
        {
            ((CustomDataGrid) target).OnExecutedPaste(args);
        }

        /// <summary>
        ///     This virtual method is called when ApplicationCommands.Paste command is executed.
        /// </summary>
        protected virtual void OnExecutedPaste(ExecutedRoutedEventArgs args)
        {
            if (!OnPasting(new CustomGridViewСancelableEventArgs()))
            {
                Paste();
            }
        }

        // Вариант Григория (работает)
        private void Paste()
        {
            string[][] rowData = ClipboardHelper.GetJuggedArrayFromClipboard();
            if (rowData == null) return;

            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            int minColumnDisplayIndex = (SelectionUnit != DataGridSelectionUnit.FullRow)
                ? Columns.IndexOf(CurrentColumn)
                : 0;
            int maxColumnDisplayIndex = Columns.Count - 1;

            int rowDataIndex = 0;
            for (int i = minRowIndex; i < minRowIndex + rowData.Length; i++, rowDataIndex++)
            {
                // Чтобы не падало при вставке в произвольном месте
                if (i >= Items.Count) break;

                int columnDataIndex = 0;
                for (int j = minColumnDisplayIndex;
                    j <= maxColumnDisplayIndex && columnDataIndex < rowData[rowDataIndex].Length;
                    j++, columnDataIndex++)
                {
                    DataGridColumn column = ColumnFromDisplayIndex(j);
                    column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                }
            }
        }

        #endregion

        #region Delete

        protected override void OnExecutedDelete(ExecutedRoutedEventArgs e)
        {
            if (OnDeleting(new CustomGridViewСancelableEventArgs((IEnumerable<object>) SelectedItems)))
            {
                SelectedItems.Clear();
            }
            else
            {
                base.OnExecutedDelete(e);
            }
        }

        #endregion

        #region Events

        #region Pasting

        public event EventHandler<CustomGridViewСancelableEventArgs> Pasting;

        /// <summary>
        ///     Вызов события об отмене действия
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Флаг отмены копирования</returns>
        protected virtual bool OnPasting(CustomGridViewСancelableEventArgs e)
        {
            if (Pasting == null) return false;

            Pasting.Invoke(this, e);

            return e.Cancel;
        }

        #endregion

        #region Deleting

        public event EventHandler<CustomGridViewСancelableEventArgs> Deleting;

        protected virtual bool OnDeleting(CustomGridViewСancelableEventArgs e)
        {
            if (Deleting == null) return false;

            Deleting.Invoke(this, e);

            return e.Cancel;
        }

        #endregion

        #region Deleted

        public event EventHandler<CustomGridViewItemsEventArgs> Deleted;

        protected virtual void OnDeleted(CustomGridViewItemsEventArgs e)
        {
            EventHandler<CustomGridViewItemsEventArgs> handler = Deleted;
            if (handler != null) handler(this, e);
        }

        #endregion

        #endregion
    }

    #region Helper classes

    public class CustomGridViewItemsEventArgs : RoutedEventArgs
    {
        public CustomGridViewItemsEventArgs()
        {
        }

        public CustomGridViewItemsEventArgs(IEnumerable<object> items)
        {
            Items = items;
        }

        public IEnumerable<object> Items { get; private set; }
    }

    public class CustomGridViewСancelableEventArgs : CustomGridViewItemsEventArgs
    {
        public CustomGridViewСancelableEventArgs()
        {
        }

        public CustomGridViewСancelableEventArgs(IEnumerable<object> items)
            : base(items)
        {
        }

        public bool Cancel { get; set; }
    }

    #endregion
}
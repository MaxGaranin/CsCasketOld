using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfSamples40.Utils;

namespace WpfSamples40.Controls.DataGridControl
{
    public class CustomDataGrid : DataGrid
    {
        public CustomDataGrid()
        {
            ContextMenu = BuildContextMenu();

            CommandManager.RegisterClassCommandBinding(
                typeof(CustomDataGrid),
                new CommandBinding(ApplicationCommands.Paste, OnExecutedPaste, OnCanExecutePaste));
        }

        #region Clipboard Paste

        private static void OnCanExecutePaste(object target, CanExecuteRoutedEventArgs args)
        {
            ((CustomDataGrid)target).OnCanExecutePaste(args);
        }

        /// <summary>
        /// This virtual method is called when ApplicationCommands.Paste command query its state.
        /// </summary>
        protected virtual void OnCanExecutePaste(CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
            args.Handled = true;
        }

        private static void OnExecutedPaste(object target, ExecutedRoutedEventArgs args)
        {
            ((CustomDataGrid)target).OnExecutedPaste(args);
        }

        /// <summary>
        /// This virtual method is called when ApplicationCommands.Paste command is executed.
        /// </summary>
        protected virtual void OnExecutedPaste(ExecutedRoutedEventArgs args)
        {
            Paste();
        }

        // Вариант Григория (работает)
        private void Paste()
        {
            var rowData = ClipboardHelper.GetJuggedArrayFromClipboard();
            if (rowData == null) return;

            var minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            var minColumnDisplayIndex = (SelectionUnit != DataGridSelectionUnit.FullRow) 
                ? Columns.IndexOf(CurrentColumn) : 0;
            var maxColumnDisplayIndex = Columns.Count - 1;

            var rowDataIndex = 0;
            for (var i = minRowIndex; i < minRowIndex + rowData.Length; i++, rowDataIndex++)
            {
                var columnDataIndex = 0;
                for (var j = minColumnDisplayIndex;
                    j <= maxColumnDisplayIndex && columnDataIndex < rowData[rowDataIndex].Length;
                    j++, columnDataIndex++)
                {
                    var column = ColumnFromDisplayIndex(j);
                    column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                }
            }
        }

        // Исходный вариант (тоже есть проблемы со вставкой новых значений)
        private void Paste2()
        {
            var rowData = ClipboardHelper.GetJuggedArrayFromClipboard();
            if (rowData == null) return;

            bool hasAddedNewRow = false;

            // call OnPastingCellClipboardContent for each cell
            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            int minColumnDisplayIndex = (SelectionUnit != DataGridSelectionUnit.FullRow) ? Columns.IndexOf(CurrentColumn) : 0;
            int maxColumnDisplayIndex = Columns.Count - 1;
            int rowDataIndex = 0;
            for (int i = minRowIndex; i <= maxRowIndex && rowDataIndex < rowData.Length; i++, rowDataIndex++)
            {
                if (i == maxRowIndex)
                {
                    // add a new row to be pasted to
                    ICollectionView cv = CollectionViewSource.GetDefaultView(Items);
                    IEditableCollectionView iecv = cv as IEditableCollectionView;
                    if (iecv != null)
                    {
                        hasAddedNewRow = true;
                        iecv.AddNew();
                            
                        if (rowDataIndex + 1 < rowData.Length)
                        {
                            // still has more items to paste, update the maxRowIndex
                            maxRowIndex = Items.Count - 1;
                        }
                    }
                }

                int columnDataIndex = 0;
                for (int j = minColumnDisplayIndex; 
                    j <= maxColumnDisplayIndex && columnDataIndex < rowData[rowDataIndex].Length; 
                    j++, columnDataIndex++)
                {
                    DataGridColumn column = ColumnFromDisplayIndex(j);
                    column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                }
            }

            // update selection
            if (hasAddedNewRow)
            {
                UnselectAll();
                UnselectAllCells();

                CurrentItem = Items[minRowIndex];

                if (SelectionUnit == DataGridSelectionUnit.FullRow)
                {
                    SelectedItem = Items[minRowIndex];
                }
                else if (SelectionUnit == DataGridSelectionUnit.CellOrRowHeader ||
                         SelectionUnit == DataGridSelectionUnit.Cell)
                {
                    SelectedCells.Add(new DataGridCellInfo(Items[minRowIndex], Columns[minColumnDisplayIndex]));
                }
            }
        }

        // Это метод странно вставляет данные - по одному ряду на значение.
        private void Paste3()
        {
            // 2-dim array containing clipboard data
            string[][] clipboardData = ClipboardHelper.GetJuggedArrayFromClipboard();

            // the index of the first DataGridRow
            int startRow = ItemContainerGenerator.IndexFromContainer(
                ItemContainerGenerator.ContainerFromItem(CurrentCell.Item));

            // the destination rows 
            //  (from startRow to either end or length of clipboard rows)
            DataGridRow[] rows =
                Enumerable.Range(startRow, Math.Min(Items.Count, clipboardData.Length))
                .Select(rowIndex => ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow)
                .Where(a => a != null)
                .ToArray();

            // the destination columns 
            //  (from selected row to either end or max. length of clipboard colums)
            DataGridColumn[] columns = Columns
                .OrderBy(column => column.DisplayIndex)
                .SkipWhile(column => column != CurrentCell.Column)
                .Take(clipboardData.Max(row => row.Length)).ToArray();

            for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                string[] rowContent = clipboardData[rowIndex];
                for (int colIndex = 0; colIndex < columns.Length; colIndex++)
                {
                    string cellContent = colIndex >= rowContent.Length 
                        ? "" 
                        : rowContent[colIndex];

                    columns[colIndex].OnPastingCellClipboardContent(rows[rowIndex].Item, cellContent);
                }
            }
        }

        #endregion

        #region ContextMenu
        
        private ContextMenu BuildContextMenu()
        {
            var menu = new ContextMenu();
            CreateMenuElements(menu);
            return menu;
        }

        private void CreateMenuElements(ContextMenu menu)
        {
            var paste = new MenuItem { Header = "Вставить" };
            paste.Click += (s, e) => Paste();
            menu.Items.Add(paste);
        }

        #endregion
    }
}

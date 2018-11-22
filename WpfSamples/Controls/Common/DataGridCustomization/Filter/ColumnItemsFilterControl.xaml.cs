using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using Telerik.Windows.Controls;
using WpfSamples.Properties;

namespace WpfSamples.Controls.Common.DataGridCustomization.Filter
{
    public partial class ColumnItemsFilterControl : INotifyPropertyChanged
    {
        #region Fields

        private CustomDataGrid _grid;
        private DataGridColumn _column;

        #endregion

        #region Constructors

        public ColumnItemsFilterControl()
        {
            InitializeComponent();
            DataContext = this;
            FilterItems = null;
        }

        #endregion

        #region  Attached Properties

        #region IsFiltered

        public static readonly DependencyProperty IsFilteredProperty =
            DependencyProperty.RegisterAttached(
                "IsFiltered",
                typeof (bool),
                typeof (ColumnItemsFilterControl),
                new PropertyMetadata(false)
                );

        public static void SetIsFiltered(DataGridColumn column, bool value)
        {
            column.SetValue(IsFilteredProperty, value);
        }

        public static bool GetIsFiltered(DataGridColumn column)
        {
            return (bool) column.GetValue(IsFilteredProperty);
        }

        #endregion

        #region IsFilterEnable

        public static readonly DependencyProperty IsFilterEnableProperty =
            DependencyProperty.RegisterAttached(
                "IsFilterEnable",
                typeof (bool),
                typeof (ColumnItemsFilterControl),
                new PropertyMetadata(false)
                );

        public static void SetIsFilterEnable(DataGridColumn column, bool value)
        {
            column.SetValue(IsFilterEnableProperty, value);
        }

        public static bool GetIsFilterEnable(DataGridColumn column)
        {
            return (bool) column.GetValue(IsFilterEnableProperty);
        }

        #endregion

        #region FiltersContainer

        public static readonly DependencyProperty FiltersContainerProperty =
            DependencyProperty.RegisterAttached(
                "FiltersContainer",
                typeof (FiltersContainer),
                typeof (ColumnItemsFilterControl),
                new PropertyMetadata(null)
                );

        public static void SetFiltersContainer(DataGrid grid, FiltersContainer value)
        {
            grid.SetValue(FiltersContainerProperty, value);
        }

        public static FiltersContainer GetFiltersContainer(DataGrid grid)
        {
            return (FiltersContainer) grid.GetValue(FiltersContainerProperty);
        }

        #endregion

        #endregion

        #region Properties

        #region FilterItems

        private ICollectionView _filterItems;

        public ICollectionView FilterItems
        {
            get { return _filterItems; }

            set
            {
                if (Equals(_filterItems, value)) return;
                _filterItems = value;
                RaisePropertyChanged("FilterItems");
            }
        }

        #endregion

        #region SearchContent

        private string _searchContent;

        public string SearchContent
        {
            get { return _searchContent; }
            set
            {
                if (Equals(_searchContent, value)) return;
                _searchContent = value;

                RaisePropertyChanged("SearchContent");

                ((ListCollectionView) FilterItems).CustomSort = null;

                if (string.IsNullOrEmpty(value))
                {
                    FilterItems.Filter = null;
                }
                else
                {
                    FilterItems.Filter = r => ((FilterItem) r).Name.ToLower().Contains(value.ToLower());
                }
            }
        }

        #endregion

        #endregion

        #region Private Methods

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            _grid = this.GetVisualParent<CustomDataGrid>();
            var header = this.GetVisualParent<DataGridColumnHeader>();
            _column = header.Column;

            var listCollectionView = _grid.Items.SourceCollection as ListCollectionView;
            if (listCollectionView != null && (listCollectionView.IsAddingNew || listCollectionView.IsEditingItem))
            {
                IsOpen = false;
                return;
            }

            if (GetFiltersContainer(_grid) == null)
            {
                var container = new FiltersContainer();
                SetFiltersContainer(_grid, container);
            }

            if (_column == null) return;

            var itemsOfSourceCollection = ((ListCollectionView) _grid.Items.SourceCollection).SourceCollection.Cast<object>();

            var namesCollection = GetNamesOfItemsFromSourceCollection(itemsOfSourceCollection);

            if (namesCollection == null) return;

            var filterItems = namesCollection.ConvertAll(r => new FilterItem(r) {IsChecked = true});

            ResetToLastSelectedItemsOfFiltering(filterItems);

            FilterItems = CollectionViewSource.GetDefaultView(filterItems);
            FilterItems.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            SearchContent = "";
        }

        private List<string> GetNamesOfItemsFromSourceCollection(IEnumerable<object> sourceItems)
        {
            var boundColumn = _column as DataGridBoundColumn;
            if (boundColumn != null)
            {
                var binding = boundColumn.Binding as Binding;
                return sourceItems.Select(r => GetValueFromItemUseBinding(r, binding)).Distinct().OrderBy(r => r).ToList();
            }

            var customColumnWithFiltering = _column as ICustomColumnWithFiltering;
            if (customColumnWithFiltering != null)
            {
                return
                    sourceItems.Select(r => customColumnWithFiltering.GetNameOfItem(r))
                        .Where(r => !string.IsNullOrEmpty(r))
                        .Distinct()
                        .OrderBy(r => r)
                        .ToList();
            }

            return null;
        }

        private void ResetToLastSelectedItemsOfFiltering(List<FilterItem> filterItems)
        {
            var container = GetFiltersContainer(_grid);

            var lastSelectedItems = container.GetLastSelectedItems(_column.Header.ToString());
            if (lastSelectedItems != null)
                filterItems.ForEach(r => r.IsChecked = lastSelectedItems.Contains(r.Name));
        }

        private string GetValueFromItemUseBinding(object data, Binding binding)
        {
            var result = BindingExpressionHelper.GetValue(data, binding);
            return result != null ? result.ToString() : null;
        }

        private void UpdateFilterOnGrid()
        {
            var filterItems = FilterItems.SourceCollection.OfType<FilterItem>().ToList();
            var container = GetFiltersContainer(_grid);
            var isNeedFiltering = filterItems.Any(r => !r.IsChecked);

            var listCollectionView = ((ListCollectionView) _grid.Items.SourceCollection);
            if (listCollectionView.IsEditingItem || listCollectionView.IsAddingNew) return;

            SetIsFiltered(_column, isNeedFiltering);

            if (!isNeedFiltering)
            {
                container.RemoveLastSelectedItems(_column.Header.ToString());
                MakeFilter(container);
                return;
            }

            var namesOfItemsForFiltering = filterItems.Where(r => r.IsChecked).Select(r => r.Name).ToList();

            Predicate<object> predicate = null;

            if (_column is DataGridBoundColumn)
            {
                var boundColumn = _column as DataGridBoundColumn;
                var binding = boundColumn.Binding as Binding;

                predicate =
                    r => namesOfItemsForFiltering.Contains(GetValueFromItemUseBinding(r, binding));
            }

            if (_column is ICustomColumnWithFiltering)
            {
                var customColumnWithFiltering = _column as ICustomColumnWithFiltering;

                predicate =
                    r => namesOfItemsForFiltering.Contains(customColumnWithFiltering.GetNameOfItem(r));
            }

            container.UpdateLastSelectedItems(_column.Header.ToString(), namesOfItemsForFiltering, predicate);
            MakeFilter(container);
        }

        private void MakeFilter(FiltersContainer container)
        {
            var listCollectionView = ((ListCollectionView) _grid.Items.SourceCollection);

            listCollectionView.CustomSort = null;
            _grid.Columns.ToList().ForEach(r => r.SortDirection = null);
            _grid.Items.Filter += container.MakeFilterOnGrid;
        }

        private void SelectAllItemsButtonClicked(object sender, RoutedEventArgs e)
        {
            FilterItems.SourceCollection.OfType<FilterItem>().ToList().ForEach(r => r.IsChecked = true);
        }

        private void UnselectAllItemsButtonClicked(object sender, RoutedEventArgs e)
        {
            FilterItems.SourceCollection.OfType<FilterItem>().ToList().ForEach(r => r.IsChecked = false);
        }

        private void OkButtonClicked(object sender, RoutedEventArgs e)
        {
            UpdateFilterOnGrid();
            IsOpen = false;
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = (Width + e.HorizontalChange > MinWidth) ? Width + e.HorizontalChange : MinWidth;
            Height = (Height + e.VerticalChange > MinHeight) ? Height + e.VerticalChange : MinHeight;
        }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    internal interface ICustomColumnWithFiltering
    {
        string GetNameOfItem(object item);
    }

    public class FilterItem : ObservableObject
    {
        public FilterItem(string name)
        {
            Name = name;
        }

        #region Name

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }

        #endregion

        #region IsChecked

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { Set("IsChecked", ref _isChecked, value); }
        }

        #endregion
    }

    public class FiltersContainer
    {
        private readonly Dictionary<string, FilterDefinition> _lastSelectedItems;

        public FiltersContainer()
        {
            _lastSelectedItems = new Dictionary<string, FilterDefinition>();
        }

        public List<string> GetLastSelectedItems(string columnName)
        {
            return !_lastSelectedItems.ContainsKey(columnName) ? null : _lastSelectedItems[columnName].ItemsForFilter;
        }

        public void UpdateLastSelectedItems(string columnName, List<string> itemsForFilter, Predicate<object> predicate)
        {
            if (_lastSelectedItems.ContainsKey(columnName))
            {
                var definition = _lastSelectedItems[columnName];
                definition.ItemsForFilter = itemsForFilter;
                definition.Predicate = predicate;
            }
            else
            {
                _lastSelectedItems.Add(columnName, new FilterDefinition(itemsForFilter, predicate));
            }
        }

        public bool MakeFilterOnGrid(object item)
        {
            return _lastSelectedItems.Values.All(filterDefinition => filterDefinition.Predicate.Invoke(item));
        }

        public void RemoveLastSelectedItems(string columnName)
        {
            _lastSelectedItems.Remove(columnName);
        }

        public void ResetAllFilters()
        {
            _lastSelectedItems.Clear();
        }
    }

    public class FilterDefinition
    {
        public FilterDefinition(List<string> itemsForFilter, Predicate<object> predicate)
        {
            ItemsForFilter = itemsForFilter;
            Predicate = predicate;
        }

        public List<string> ItemsForFilter { get; set; }

        public Predicate<object> Predicate { get; set; }
    }
}
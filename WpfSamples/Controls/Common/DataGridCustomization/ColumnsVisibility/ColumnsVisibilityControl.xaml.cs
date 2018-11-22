using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using Telerik.Windows.Controls;

namespace WpfSamples.Controls.Common.DataGridCustomization.ColumnsVisibility
{
    public partial class ColumnsVisibilityControl : INotifyPropertyChanged
    {
        private CustomDataGrid _grid;

        public ColumnsVisibilityControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            _grid = this.GetVisualParent<CustomDataGrid>();
            ColumnsItems = _grid.Columns
                .Select(r => new ColumnsVisibilityContainer(r))
                .ToList();
        }

        #region IsEnableColumnsVisibility

        public static readonly DependencyProperty IsEnableColumnsVisibilityProperty =
            DependencyProperty.RegisterAttached("IsEnableColumnsVisibility", typeof (bool),
                typeof (ColumnsVisibilityControl), new PropertyMetadata(false));

        public static void SetIsEnableColumnsVisibility(DataGrid grid, bool value)
        {
            grid.SetValue(IsEnableColumnsVisibilityProperty, value);
        }

        public static bool GetIsEnableColumnsVisibility(DataGrid grid)
        {
            return (bool) grid.GetValue(IsEnableColumnsVisibilityProperty);
        }

        #endregion

        #region ColumnsItems

        private IList<ColumnsVisibilityContainer> _columnsItems;

        public IList<ColumnsVisibilityContainer> ColumnsItems
        {
            get { return _columnsItems; }

            set
            {
                if (Equals(_columnsItems, value)) return;
                _columnsItems = value;
                RaisePropertyChanged("ColumnsItems");
            }
        }

        #endregion

        #region Event habdlers

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }

        #endregion
        
        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Class ColumnsVisibilityContainer

        public class ColumnsVisibilityContainer : ObservableObject
        {
            public DataGridColumn Column { get; private set; }

            public ColumnsVisibilityContainer(DataGridColumn column)
            {
                Column = column;
            }

            public bool IsVisible
            {
                get { return Column.Visibility == Visibility.Visible; }

                set
                {
                    Column.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                    RaisePropertyChanged("IsVisible");
                }
            }
        }

        #endregion
    }
}
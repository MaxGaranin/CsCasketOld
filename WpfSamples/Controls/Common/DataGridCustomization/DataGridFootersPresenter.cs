using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfSamples40.Controls.Common.DataGridCustomization
{
    public class DataGridFootersPresenter : ItemsControl
    {
        #region Constructors

        public DataGridFootersPresenter()
        {
            Background = new SolidColorBrush(Colors.Transparent);

            var elementFactory = new FrameworkElementFactory(typeof (StackPanel));
            elementFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            ItemsPanel = new ItemsPanelTemplate {VisualTree = elementFactory};
        }

        #endregion

        #region Dependency properties

        #region dependency property TargetDataGrid

        public static readonly DependencyProperty TargetDataGridProperty =
            DependencyProperty.Register("TargetDataGrid", typeof (DataGrid), typeof (DataGridFootersPresenter),
                new PropertyMetadata(null, TargetDataGridChanged));

        public DataGrid TargetDataGrid
        {
            get { return (DataGrid) GetValue(TargetDataGridProperty); }
            set { SetValue(TargetDataGridProperty, value); }
        }

        #endregion

        #region attached property FooterContent

        public static readonly DependencyProperty FooterContentProperty =
            DependencyProperty.RegisterAttached("FooterContent", typeof (string),
                typeof (DataGridFootersPresenter), new PropertyMetadata(default(string)));

        public static void SetFooterContent(DependencyObject elementDependencyObject, string value)
        {
            elementDependencyObject.SetValue(FooterContentProperty, value);
        }

        public static string GetFooterContent(DependencyObject elementDependencyObject)
        {
            return (string) elementDependencyObject.GetValue(FooterContentProperty);
        }

        #endregion

        #region attached property FooterColumnId

        public static readonly DependencyProperty FooterColumnIdProperty =
            DependencyProperty.RegisterAttached(
                "FooterColumnId",
                typeof (string),
                typeof (DataGridFootersPresenter),
                new PropertyMetadata(null)
                );

        public static void SetFooterColumnId(DataGridColumn elementNameColumn, string value)
        {
            elementNameColumn.SetValue(FooterColumnIdProperty, value);
        }

        public static string GetFooterColumnId(DataGridColumn elementNameColumn)
        {
            return (string) elementNameColumn.GetValue(FooterColumnIdProperty);
        }

        #endregion

        #region attached property FooterContainer

        public static readonly DependencyProperty FooterContainerProperty =
            DependencyProperty.RegisterAttached("FooterContainer", typeof (FooterData),
                typeof (DataGridFootersPresenter), new PropertyMetadata(default(FooterData)));

        public static void SetFooterContainer(DependencyObject elementDependencyObject, FooterData value)
        {
            elementDependencyObject.SetValue(FooterContainerProperty, value);
        }

        public static FooterData GetFooterContainer(DependencyObject elementDependencyObject)
        {
            return (FooterData) elementDependencyObject.GetValue(FooterContainerProperty);
        }

        #endregion

        #endregion

        #region HorizontalOffset

        public double HorizontalOffset
        {
            get { return (double) GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof (double), typeof (DataGridFootersPresenter),
                new PropertyMetadata(0.0d, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var presenter = dependencyObject as DataGridFootersPresenter;
            if (presenter != null) presenter.InvalidateArrange();
        }

        #endregion

        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElement uiElement = VisualTreeHelper.GetChildrenCount(this) > 0
                ? VisualTreeHelper.GetChild(this, 0) as UIElement
                : null;
            if (uiElement != null)
            {
                Rect finalRect = new Rect(finalSize);
                DataGrid parentDataGrid = TargetDataGrid;
                if (parentDataGrid != null)
                {
                    var value = (double) GetInstanceField(typeof (DataGrid), parentDataGrid, "HorizontalScrollOffset");
                    var value2 = (double) GetInstanceField(typeof (DataGrid), parentDataGrid, "CellsPanelActualWidth");
                    finalRect.X = -value;
                    finalRect.Width = Math.Max(finalSize.Width, value2);
                }
                uiElement.Arrange(finalRect);
            }
            return finalSize;
        }

        private static object GetInstanceField(Type type, object instance, string fieldName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                           | BindingFlags.Static;

            var field = type.GetProperty(fieldName, bindFlags);
            return field.GetValue(instance, null);
        }

        private static void TargetDataGridChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var footersPresenter = dependencyObject as DataGridFootersPresenter;
            Debug.Assert(footersPresenter != null, "footersPresenter != null");

            var newGrid = dependencyPropertyChangedEventArgs.NewValue as DataGrid;
            Debug.Assert(newGrid != null, "newGrid != null");

            FooterData conatiner = GetFooterContainer(newGrid);

            foreach (DataGridColumn column in newGrid.Columns)
            {
                var columnFooterContent = new TextBlock
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                    FontWeight = FontWeights.SemiBold
                };

                string conent = GetFooterContent(column);
                if (!string.IsNullOrWhiteSpace(conent))
                {
                    columnFooterContent.Text = conent;
                }

                string columnId = GetFooterColumnId(column);

                if (!string.IsNullOrWhiteSpace(columnId))
                {
                    columnFooterContent.SetBinding(TextBlock.TextProperty,
                        new Binding(string.Format("[{0}]", columnId)) {Source = conatiner});
                }

                var border = new Border
                {
                    BorderThickness = new Thickness(0.0f, 0.0f, 0.0f, 0.0f),
                    BorderBrush = new SolidColorBrush(Colors.LightGray)
                };

                var actualWidthBinding = new Binding("ActualWidth") {Source = column};

                border.SetBinding(WidthProperty, actualWidthBinding);

                border.Child = columnFooterContent;

                footersPresenter.Items.Add(border);
            }
        }
    }
}
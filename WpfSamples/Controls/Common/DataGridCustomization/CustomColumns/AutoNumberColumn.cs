using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfSamples.Controls.Common.DataGridCustomization.CustomColumns
{
    public class AutoNumberColumn : DataGridBoundColumn
    {
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var textBox = new TextBlock();
            textBox.SetBinding(TextBlock.TextProperty, new Binding(".")
            {
                Converter = new IndexOfItemsConverter(DataGridOwner.Items)
            });

            ClipboardContentBinding = new Binding(".")
            {
                Converter = new IndexOfItemsConverter(DataGridOwner.Items)
            };
            return textBox;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return cell.Content as FrameworkElement;
        }
    }

    public class IndexOfItemsConverter : IValueConverter
    {
        private readonly ItemCollection _items;

        public IndexOfItemsConverter(ItemCollection items)
        {
            _items = items;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _items.IndexOf(value) + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
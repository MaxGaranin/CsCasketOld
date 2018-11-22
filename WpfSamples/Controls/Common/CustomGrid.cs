using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfSamples.Controls.Common
{
    public class CustomGrid : Grid
    {
        public CustomGrid()
        {
            this.Loaded += new RoutedEventHandler(CustomGrid_Loaded);
        }

        private void CustomGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetIsEnabledOfChildren();
        }

        private void SetIsEnabledOfChildren()
        {
            foreach (UIElement child in this.Children)
            {
                var readOnlyProperty = child.GetType().GetProperties()
                    .FirstOrDefault(prop => prop.Name.Equals("IsReadOnly"));

                if (readOnlyProperty != null)
                    readOnlyProperty.SetValue(child, this.IsReadOnly, null);
            }
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(CustomGrid),
            new PropertyMetadata(new PropertyChangedCallback(OnIsReadOnlyChanged)));

        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomGrid)d).OnIsReadOnlyChanged(e);
        }

        protected virtual void OnIsReadOnlyChanged(DependencyPropertyChangedEventArgs e)
        {
            this.SetIsEnabledOfChildren();
        }
    }
}

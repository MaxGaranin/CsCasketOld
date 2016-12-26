using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfSamples40.Controls.Common
{
    public class CustomStackPanel : StackPanel
    {
        public CustomStackPanel()
        {
            this.Loaded += new RoutedEventHandler(CustomStackPanel_Loaded);
        }

        private void CustomStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetIsEnabledOfChildren(this);
        }

        private void SetIsEnabledOfChildren(FrameworkElement element)
        {
            var readOnlyProperty = element.GetType().GetProperties()
                .FirstOrDefault(prop => prop.Name.Equals("IsReadOnly"));

            if (readOnlyProperty != null)
                readOnlyProperty.SetValue(element, this.IsReadOnly, null);

            var children = LogicalTreeHelper.GetChildren(element);
            foreach (var child in children)
            {
                if (!(child is FrameworkElement)) continue;
                SetIsEnabledOfChildren((FrameworkElement) child);
            }
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(CustomStackPanel),
                new PropertyMetadata(new PropertyChangedCallback(OnIsReadOnlyChanged)));

        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomStackPanel)d).OnIsReadOnlyChanged(e);
        }

        protected virtual void OnIsReadOnlyChanged(DependencyPropertyChangedEventArgs e)
        {
            this.SetIsEnabledOfChildren(this);
        }
 
    }
}
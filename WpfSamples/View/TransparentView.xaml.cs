using System.Windows;
using System.Windows.Input;

namespace WpfSamples40.View
{
    public partial class TransparentView : Window
    {
        public TransparentView()
        {
            InitializeComponent();
        }

        private void TransparentView_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TransparentView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}

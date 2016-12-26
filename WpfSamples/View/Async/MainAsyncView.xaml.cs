using System;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace WpfSamples40.View.Async
{
    public partial class MainAsyncView : Window
    {
        public MainAsyncView()
        {
            InitializeComponent();
        }

        private void OpenDialogButton_OnClick(object sender, RoutedEventArgs e)
        {
            DispatcherHelper.UIDispatcher.BeginInvoke(
                new Action(() => { new SomeDialogView().ShowDialog(); }));

            Console.WriteLine("Further execution...");
        }
    }
}
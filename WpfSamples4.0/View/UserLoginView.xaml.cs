using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfSamples40.View
{
    /// <summary>
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : Window
    {
        public UserLoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            executeButton.IsEnabled = false;
            Task<int>.Factory.StartNew(() =>
            {
                return Auth.Login();
            })
            .ContinueWith(task => //Выполнить код в основном потоке (TaskScheduler.FromCurrentSynchronizationContext())
            {
                executeButton.IsEnabled = true;
                _result.Text = task.Result.ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    public static class Auth
    {
        private static int _counter;

        public static int Login()
        {
            Thread.Sleep(1000);
            return ++_counter;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSamples.View.Async.BackgroundWorkerSample
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

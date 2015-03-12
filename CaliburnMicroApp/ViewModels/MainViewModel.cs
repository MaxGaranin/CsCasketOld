using System.Windows;
using Caliburn.Micro;

namespace CaliburnMicroApp.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        public MainViewModel()
        {
            HelloWorld = "Hello World";
        }

        private string _helloWorld;

        public string HelloWorld
        {
            get { return _helloWorld; }
            set
            {
                _helloWorld = value;
                NotifyOfPropertyChange(() => HelloWorld);
            }
        }

        //Don't use this code in production
        public void ShowMessage()
        {
            MessageBox.Show("Test Caliburn.Micro");
        }         
    }
}
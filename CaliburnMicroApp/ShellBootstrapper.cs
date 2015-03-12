using System.Windows;
using Caliburn.Micro;
using CaliburnMicroApp.ViewModels;

namespace CaliburnMicroApp
{
    public class ShellBootstrapper : BootstrapperBase
    {
        public ShellBootstrapper() : base(true)
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
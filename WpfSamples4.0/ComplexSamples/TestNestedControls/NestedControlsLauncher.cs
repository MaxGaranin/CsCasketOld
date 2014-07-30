using WpfSamples40.ComplexSamples.FluidModels;

namespace WpfSamples40.ComplexSamples.TestNestedControls
{
    public class NestedControlsLauncher
    {
        public void Run()
        {
            var view = new MainView();

            var viewModel = view.DataContext as WellModelViewModel;

            viewModel.BankFluidViewModel = new BankFluidViewModel()
                {
                    SelectedBankFluidModel = new FluidModel()
                        {
                            Name = "Первая модель из банка"
                        }
                };
            view.WellFluidControl.BankControl.DataContext = viewModel.BankFluidViewModel;

            viewModel.LocalFluidViewModel = new LocalFluidViewModel()
                {
                    LocalFluidModel = new FluidModel()
                        {
                            Name = "Локальная модель по скважине 46"
                        }
                };
            view.WellFluidControl.LocalControl.DataContext = viewModel.LocalFluidViewModel;

            viewModel.IsBankModel = true;
                
            view.Show();
        }
    }
}
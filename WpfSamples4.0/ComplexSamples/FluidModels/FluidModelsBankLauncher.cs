using System.Collections.ObjectModel;
using WpfSamples40.ComplexSamples.FluidModels.Views;

namespace WpfSamples40.ComplexSamples.FluidModels
{
    public class FluidModelsBankLauncher
    {
        public void Run()
        {
            #region Init
            var fluidModelBank = new FluidModelBank();

            var defaultFluidModel = new FluidModel()
            {
                Name = "Группа по умолчанию",
                ExistsInBank = true
            };
            fluidModelBank.DefaultFluidModel = defaultFluidModel;

            fluidModelBank.FluidModelGroups = new ObservableCollection<FluidModelGroup>();
            var defaultGroup = new FluidModelGroup()
            {
                Name = "Модель по умолчанию"
            };
            defaultGroup.AddFluidModel(defaultFluidModel);
            fluidModelBank.FluidModelGroups.Add(defaultGroup);


            defaultGroup.AddFluidModel(new FluidModel() { Name = "Модель 1", ExistsInBank = true });
            defaultGroup.AddFluidModel(new FluidModel() { Name = "Модель 2", ExistsInBank = true });

            var group2 = new FluidModelGroup() { Name = "Дополнительная группа" };
            group2.AddFluidModel(new FluidModel() { Name = "aaaaaaaaaa", ExistsInBank = true });
            group2.AddFluidModel(new FluidModel() { Name = "bbbbb", ExistsInBank = true });
            group2.AddFluidModel(new FluidModel() { Name = "ccccc", ExistsInBank = true });
            fluidModelBank.FluidModelGroups.Add(group2);

            #endregion

            var view = new FluidModelBankView();
            var viewModel = new FluidModelBankViewModel();
            viewModel.FluidModelBank = fluidModelBank;
            view.DataContext = viewModel;

            view.Show();
        }


    }
}
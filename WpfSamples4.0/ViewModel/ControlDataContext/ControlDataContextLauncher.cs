using WpfSamples40.View.ControlDataContext;

namespace WpfSamples40.ViewModel.ControlDataContext
{
    public class ControlDataContextLauncher
    {
        public static void Run()
        {
            var view = new ControlDataContextView();
            var viewModel = new ComplexViewModel()
            {
                FirstViewModel = new FirstViewModel() { IntValue = 5},
                SecondViewModel = new SecondViewModel() { StringValue = "Привет!"}
            };
            view.DataContext = viewModel;

            view.Show();
        }
    }
}
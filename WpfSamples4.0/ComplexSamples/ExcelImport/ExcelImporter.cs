using System.Collections.Generic;

namespace WpfSamples40.ComplexSamples.ExcelImport
{
    public class ExcelImporter
    {
        public string[,] DataArray { get; private set; }

        public bool Process(IEnumerable<string> names)
        {
            var view = new ExcelImporterView();
            var viewModel = new ExcelImporterViewModel(names);
            view.DataContext = viewModel;

            view.ShowDialog();

            if (!viewModel.DialogResult) return false;

            DataArray = viewModel.ResultArray;
            return true;
        }
    }
}
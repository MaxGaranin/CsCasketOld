using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace WpfSamples40.ComplexSamples.FluidModels
{
    public class FluidModelEditorViewModel : ViewModelBase
    {
        private FluidModel _fluidModel;
        private IEnumerable<FluidModelGroup> _groups;
        private string _title;

        public FluidModel FluidModel
        {
            get { return _fluidModel; }
            set
            {
                _fluidModel = value;
                RaisePropertyChanged("FluidModel");
            }
        }

        public IEnumerable<FluidModelGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }
    }
}
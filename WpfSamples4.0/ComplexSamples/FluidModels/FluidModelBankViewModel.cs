using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Omu.ValueInjecter;
using WpfSamples40.ComplexSamples.FluidModels.Views;
using WpfSamples40.Utils;

namespace WpfSamples40.ComplexSamples.FluidModels
{
        public class FluidModelBankEventArgs : EventArgs
    {
        public FluidModelBank Bank { get; set; }
    }

    public class FluidModelBankViewModel : ViewModelBase
    {
        private FluidModelBank _fluidModelBank;
        private ObservableCollection<FluidModelGroup> _groups;
        private ReadOnlyObservableCollection<FluidModel> _fluidModels;
        private FluidModelGroup _currentGroup;
        private FluidModel _currentFluidModel;

        public FluidModelBank FluidModelBank
        {
            get { return _fluidModelBank; }
            set
            {
                _fluidModelBank = value;
                RaisePropertyChanged("FluidModelBank");

                if (_fluidModelBank == null) return;
                Groups = _fluidModelBank.FluidModelGroups;
            }
        }

        public ObservableCollection<FluidModelGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");

                CurrentGroup = _groups.FirstOrDefault();
            }
        }

        public ReadOnlyObservableCollection<FluidModel> FluidModels
        {
            get { return _fluidModels; }
            set
            {
                _fluidModels = value;
                RaisePropertyChanged("FluidModels");

                CurrentFluidModel = _fluidModels.FirstOrDefault();
            }
        }

        public FluidModelGroup CurrentGroup
        {
            get { return _currentGroup; }
            set
            {
                Unsubscribe(_currentGroup);

                _currentGroup = value;
                RaisePropertyChanged("CurrentGroup");

                Subscribe(_currentGroup);

                if (_currentGroup == null) return;
                FluidModels = _currentGroup.FluidModels;
            }
        }

        private void Subscribe(FluidModelGroup group)
        {
            if (group == null) return;
            ((INotifyCollectionChanged) group.FluidModels).CollectionChanged += SourceItems_CollectionChanged;
        }

        private void Unsubscribe(FluidModelGroup group)
        {
            if (group == null) return;
            ((INotifyCollectionChanged) group.FluidModels).CollectionChanged -= SourceItems_CollectionChanged;
        }

        private void SourceItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FluidModel result = null;
            if (e.NewItems != null)
            {
                result = (FluidModel) e.NewItems[0];
            }
            else if (e.OldItems != null)
            {
                result = (FluidModel) e.OldItems[0];

            }
            Console.WriteLine("Changed, result = {0}", result);
        }

        public FluidModel CurrentFluidModel
        {
            get { return _currentFluidModel; }
            set
            {
                _currentFluidModel = value;
                RaisePropertyChanged("CurrentFluidModel");
            }
        }

        #region CreateModelCommand

        private RelayCommand _createModelCommand;

        public RelayCommand CreateModelCommand
        {
            get
            {
                if (_createModelCommand == null)
                    _createModelCommand = new RelayCommand(CreateModel);
                return _createModelCommand;
            }
        }

        public void CreateModel()
        {
            var view = new FluidModelEditorView();
            var viewModel = new FluidModelEditorViewModel();

            viewModel.Title = "Новая модель жидкости";
            viewModel.FluidModel = new FluidModel();
            viewModel.FluidModel.FluidModelGroup = CurrentGroup;
            viewModel.Groups = Groups;

            view.DataContext = viewModel;
            var result = view.ShowDialog();
            if (result == true)
            {
                FluidModelBank.AddModelWithGroup(viewModel.FluidModel);
                CurrentGroup = viewModel.FluidModel.FluidModelGroup;
                CurrentFluidModel = viewModel.FluidModel;
            }
        }

        #endregion

        #region EditModelCommand

        private RelayCommand _editModelCommand;

        public RelayCommand EditModelCommand
        {
            get
            {
                if (_editModelCommand == null)
                    _editModelCommand = new RelayCommand(EditModel);
                return _editModelCommand;
            }
        }

        public void EditModel()
        {
            var view = new FluidModelEditorView();
            var viewModel = new FluidModelEditorViewModel();

            viewModel.Title = "Редактирование модели жидкости";
            viewModel.FluidModel = CurrentFluidModel.DeepClone();
            viewModel.Groups = Groups;

            view.DataContext = viewModel;
            var result = view.ShowDialog();
            if (result == true)
            {
                var modelIndex = FluidModels.IndexOf(CurrentFluidModel);
                FluidModels[modelIndex].InjectFrom(viewModel.FluidModel);
                CurrentFluidModel = null;
                CurrentFluidModel = FluidModels[modelIndex];

                if (!viewModel.FluidModel.FluidModelGroup.Equals(CurrentGroup))
                {
                    CurrentGroup.RemoveFluidModel(FluidModels[modelIndex]);

                    var groupIndex = Groups.IndexOf(viewModel.FluidModel.FluidModelGroup);
                    Groups[groupIndex].AddFluidModel(viewModel.FluidModel);

                    CurrentGroup = null;
                    CurrentGroup = Groups[groupIndex];
                }

            }
        }

        #endregion

        #region RemoveModelCommand

        private RelayCommand _removeModelCommand;

        public RelayCommand RemoveModelCommand
        {
            get
            {
                if (_removeModelCommand == null)
                    _removeModelCommand = new RelayCommand(RemoveModel);
                return _removeModelCommand;
            }
        }

        public void RemoveModel()
        {
            if (CurrentFluidModel == null) return;
            CurrentGroup.RemoveFluidModel(CurrentFluidModel);
            CurrentFluidModel = CurrentGroup.FluidModels.FirstOrDefault();
        }

        #endregion

        #region SaveBankCommand

        private RelayCommand _saveBankCommand;

        public RelayCommand SaveBankCommand
        {
            get
            {
                if (_saveBankCommand == null)
                    _saveBankCommand = new RelayCommand(SaveBank);
                return _saveBankCommand;
            }
        }

        public EventHandler<FluidModelBankEventArgs> BankSaved;

        public void SaveBank()
        {
            if (BankSaved != null) BankSaved(this, new FluidModelBankEventArgs() {Bank = FluidModelBank});
        }

        #endregion
    }
}
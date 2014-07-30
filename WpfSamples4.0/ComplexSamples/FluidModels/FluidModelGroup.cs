using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace WpfSamples40.ComplexSamples.FluidModels
{
    /// <summary>
    /// Группа моделей жидкости
    /// </summary>
    [DataContract]
    [Serializable]
    public class FluidModelGroup : INotifyPropertyChanged
    {
        private Guid _id;
        private string _name;
        private ObservableCollection<FluidModel> _fluidModels;
        private ReadOnlyObservableCollection<FluidModel> _readOnlyFluidModels;

        #region Constructor

        /// <summary>
        /// Конструктор
        /// </summary>
        public FluidModelGroup()
        {
            InitBeforeDeserializing(new StreamingContext());
        }

        [OnDeserializing]
        private void InitBeforeDeserializing(StreamingContext sc)
        {
            Id = Guid.NewGuid();

            _fluidModels = new ObservableCollection<FluidModel>();
            _readOnlyFluidModels = new ReadOnlyObservableCollection<FluidModel>(_fluidModels);
        }

        #endregion

        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        /// <summary>
        /// Название группы
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        /// <summary>
        /// Список моделей жидкости, только для чтения.
        /// Для добавления и удаления используйте AddFluidModel и RemoveFluidModel.
        /// </summary>
        [DataMember]
        public ReadOnlyObservableCollection<FluidModel> FluidModels
        {
            get { return _readOnlyFluidModels; }
        }

        /// <summary>
        /// Добавляет модель в группу
        /// </summary>
        /// <param name="fluidModel">Модель жидкости</param>
        public void AddFluidModel(FluidModel fluidModel)
        {
            if (_fluidModels.Any(m => m.Name == fluidModel.Name))
            {
                throw new InvalidOperationException("Группа уже содежит модель с данным именем");
            }

            _fluidModels.Add(fluidModel);
            fluidModel.FluidModelGroup = this;
        }

        /// <summary>
        /// Удаляет модель из группы
        /// </summary>
        /// <param name="fluidModel">Модель жидкости</param>
        /// <returns>Флаг успешности удаления</returns>
        public bool RemoveFluidModel(FluidModel fluidModel)
        {
            var result = _fluidModels.Remove(fluidModel);
            if (result)
            {
                fluidModel.FluidModelGroup = null;
            }

            return result;
        }

        #region Equality members

        protected bool Equals(FluidModelGroup other)
        {
            return _id.Equals(other._id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FluidModelGroup)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        #endregion

        #region INotifyPropertyChanged members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
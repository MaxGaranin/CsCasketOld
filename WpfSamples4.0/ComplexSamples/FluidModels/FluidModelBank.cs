using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace WpfSamples40.ComplexSamples.FluidModels
{
    /// <summary>
    /// Банк моделей жидкости
    /// </summary>
    [DataContract]
    [Serializable]
    public class FluidModelBank : INotifyPropertyChanged
    {
        public FluidModelBank()
        {
            FluidModelGroups = new ObservableCollection<FluidModelGroup>();
        }

        /// <summary>
        /// Модель жидкости по умолчанию
        /// </summary>
        [DataMember]
        public FluidModel DefaultFluidModel { get; set; }

        /// <summary>
        /// Список групп моделей жидкости
        /// </summary>
        [DataMember]
        public ObservableCollection<FluidModelGroup> FluidModelGroups { get; set; }

        /// <summary>
        /// Возвращает список всех моделей в банке
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FluidModel> GetAllFluidModels()
        {
            var fluidModels = new List<FluidModel>();
            foreach (var group in FluidModelGroups)
            {
                fluidModels.AddRange(group.FluidModels);
            }
            return fluidModels;
        }

        /// <summary>
        /// Добавляет модель с заданной группой в банк
        /// </summary>
        /// <param name="fluidModel">Модель для добавления</param>
        public void AddModelWithGroup(FluidModel fluidModel)
        {
            var group = fluidModel.FluidModelGroup;
            if (group == null)
                throw new InvalidOperationException("Невозможно добавить в банк модель без группы");

            var bankGroup = FluidModelGroups.SingleOrDefault(g => g.Name == group.Name);
            if (bankGroup == null)
                throw new InvalidOperationException(string.Format("Группа '{0}' не найдена в банке", group.Name));

            bankGroup.AddFluidModel(fluidModel);
        }

        #region INotifyPropertyChanged members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
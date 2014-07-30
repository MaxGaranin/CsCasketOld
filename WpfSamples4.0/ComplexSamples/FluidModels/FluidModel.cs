using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WpfSamples40.ComplexSamples.FluidModels
{
    /// <summary>
    /// Модель жидкости
    /// </summary>
    [DataContract]
    [Serializable]
    public class FluidModel : INotifyPropertyChanged
    {
        #region Constructor

        /// <summary>
        /// Конструктор
        /// </summary>
        public FluidModel()
        {
            InitBeforeDeserializing(new StreamingContext());
        }

        // Инициализация выделена в отдельный метод, т.к. конструктор при
        // десереализации DataContract не вызывается, а этот метод - вызывается
        [OnDeserializing]
        private void InitBeforeDeserializing(StreamingContext sc)
        {
            Id = Guid.NewGuid();

            oil_sg = 1;
            water_sg = 2;
            gas_sg = 3;
            wc_critical_percent = 3;
            gor = 4;
        }

        #endregion

        #region Main properties

        private Guid _id;
        private string _name;
        private string _group;
        private FluidModelGroup _fluidModelGroup;
        private bool _existsInBank;
        private string _description;
        private double _oilSg;
        private double _waterSg;
        private double _gasSg;
        private double _wcCriticalPercent;
        private double _gor;

        /// <summary>
        /// Идентификатор модели
        /// </summary>
        [DataMember]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        /// <summary>
        /// Название модели жидкости
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        // TODO: убрать
        /// <summary>
        /// Название группы
        /// </summary>
        [DataMember]
        [Obsolete]
        public string Group
        {
            get { return _group; }
            set { _group = value; RaisePropertyChanged("Group"); }
        }

        /// <summary>
        /// Группа модели жидкости
        /// </summary>
        [DataMember]
        public FluidModelGroup FluidModelGroup
        {
            get { return _fluidModelGroup; }
            set { _fluidModelGroup = value; RaisePropertyChanged("FluidModelGroup"); }
        }

        /// <summary>
        /// Описание модели
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("Description"); }
        }

        /// <summary>
        /// Модель из банка
        /// </summary>
        [DataMember]
        public bool ExistsInBank
        {
            get { return _existsInBank; }
            set { _existsInBank = value; RaisePropertyChanged("ExistsInBank"); }
        }

        #endregion

        #region Fluid properties

        /// <summary>
        /// Удельная плотность нефти 
        /// </summary>
        [DataMember]
        public double oil_sg
        {
            get { return _oilSg; }
            set { _oilSg = value; RaisePropertyChanged("oil_sg"); }
        }

        /// <summary>
        /// Удельная плотность воды
        /// </summary>
        [DataMember]
        public double water_sg
        {
            get { return _waterSg; }
            set { _waterSg = value; RaisePropertyChanged("water_sg"); }
        }

        /// <summary>
        /// Удельная плотность газа
        /// </summary>
        [DataMember]
        public double gas_sg
        {
            get { return _gasSg; }
            set { _gasSg = value; RaisePropertyChanged("gas_sg"); }
        }

        /// <summary>
        /// Критическая обводнённость 
        /// </summary>
        [DataMember]
        public double wc_critical_percent
        {
            get { return _wcCriticalPercent; }
            set { _wcCriticalPercent = value; RaisePropertyChanged("wc_critical_percent"); }
        }

        /// <summary>
        /// Газовый фактор
        /// </summary>
        [DataMember]
        public double gor
        {
            get { return _gor; }
            set { _gor = value; RaisePropertyChanged("gor"); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region Equality members

        protected bool Equals(FluidModel other)
        {
            return _id.Equals(other._id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FluidModel)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }


}

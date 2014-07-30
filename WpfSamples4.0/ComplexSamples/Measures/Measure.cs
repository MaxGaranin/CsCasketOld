using System;
using System.Runtime.Serialization;

namespace WpfSamples40.ComplexSamples.Measures
{
    /// <summary>
    /// Единицы измерения (размерности)
    /// </summary>
    [DataContract]
    [Serializable]
    public class Measure
    {
        public Measure()
        {
        }

        public Measure(MeasureGroupCode groupCode, MeasureCode code, string name, bool isBase,
            double convertValueK, double convertValueB, string desc)
        {
            MeasureGroupCode = groupCode;
            MeasureGroupName = groupCode.GetGroupName();
            MeasureCode = code;
            Description = desc;
            MeasureName = name;
            IsBase = isBase;
            ConvertValueK = convertValueK;
            ConvertValueB = convertValueB;
        }

        /// <summary>
        /// Код группы размерности
        /// </summary>
        [DataMember]
        public MeasureGroupCode MeasureGroupCode { get; protected set; }

        /// <summary>
        /// Название группы размерности
        /// </summary>
        [IgnoreDataMember]
        public string MeasureGroupName { get; set; }

        /// <summary>
        /// Код размерности
        /// </summary>
        [DataMember]
        public MeasureCode MeasureCode { get; protected set; }

        /// <summary>
        /// Название размерности
        /// </summary>
        [DataMember]
        public string MeasureName { get; protected set; }

        /// <summary>
        /// Признак базовой размерности
        /// </summary>
        [DataMember]
        public bool IsBase { get; protected set; }

        /// <summary>
        /// Отношение данной размерности к базовой
        /// </summary>
        [DataMember]
        public double ConvertValueK { get; protected set; }

        /// <summary>
        /// Смещение данной приведенной размерности относительно базовой
        /// </summary>
        [DataMember]
        public double ConvertValueB { get; protected set; }

        /// <summary>
        /// Описание размерности
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        #region ToString
        public override string ToString()
        {
            return Description;
        }
        #endregion

        #region Equality members

        protected bool Equals(Measure other)
        {
            return MeasureCode == other.MeasureCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Measure)obj);
        }

        public override int GetHashCode()
        {
            return (int)MeasureCode;
        } 

        #endregion
    }
}

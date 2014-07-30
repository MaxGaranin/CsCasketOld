using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfSamples40.ComplexSamples.Measures
{
    /// <summary>
    /// Менеджер единиц измерений
    /// </summary>
    public class MeasureManager
    {
        private IDictionary<MeasureGroupCode, IEnumerable<Measure>> _groupsToMeasures;

        #region Singleton
        
        private static MeasureManager _instance;
        public static MeasureManager Instance
        {
            get { return _instance ?? (_instance = new MeasureManager()); }
        }

        private MeasureManager()
        {
            InitMeasures();
        }

        #endregion

        #region InitMeasures

        private void InitMeasures()
        {
            Measures = new List<Measure>();

            #region Pressure PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Pressure, MeasureCode.Pressure_Atmosphere,
                "атм", false, 0.000009869232667160128, 0, "Физическая атмосфера(atm, атм)"));

            Measures.Add(new Measure(MeasureGroupCode.Pressure, MeasureCode.Pressure_TechAtmosphere,
                "ат", false, 0.000010197106, 0, "Техническая атмосфера (at, ат)"));

            Measures.Add(new Measure(MeasureGroupCode.Pressure, MeasureCode.Pressure_Pascal,
                "Па", true, 1, 0, "Паскаль(Pa, Па)"));

            Measures.Add(new Measure(MeasureGroupCode.Pressure, MeasureCode.Pressure_Bar,
                "Бар", false, 0.00001, 0, "Бар(бар)"));

            #endregion

            #region Lenght PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Length, MeasureCode.Length_Meter,
                "м", true, 1, 0, "Метр"));

            Measures.Add(new Measure(MeasureGroupCode.Length, MeasureCode.Length_Millimeter,
                "мм", false, 1000, 0, "Миллиметр (mm, мм)"));

            Measures.Add(new Measure(MeasureGroupCode.Length, MeasureCode.Length_Centimeter,
                "см", false, 100, 0, "Сантиметр (sm, см)"));

            Measures.Add(new Measure(MeasureGroupCode.Length, MeasureCode.Length_Kilometre,
                "км", false, 0.001, 0, "Километр (sm, см)"));

            Measures.Add(new Measure(MeasureGroupCode.Length, MeasureCode.Length_Mile,
                "миль", false, 0.000621371192, 0, "Миля (ml, миль)"));

            #endregion

            #region Volume PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Volume, MeasureCode.Volume_m3,
                "м3", true, 1, 0, "Кубометр (m3, м3)"));

            Measures.Add(new Measure(MeasureGroupCode.Volume, MeasureCode.Volume_l,
                "л", false, 1000, 0, "Литр(l, л)"));

            #endregion

            #region Time PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Time, MeasureCode.Time_s,
                "сек.", true, 1, 0, "Секунда (s, с)"));

            Measures.Add(new Measure(MeasureGroupCode.Time, MeasureCode.Time_m,
                "мин.", false, 0.166666666666667, 0, "Минута (min, мин)"));

            Measures.Add(new Measure(MeasureGroupCode.Time, MeasureCode.Time_h,
                "час.", false, 0.000277777778, 0, "Час (h, ч)"));

            Measures.Add(new Measure(MeasureGroupCode.Time, MeasureCode.Time_d,
                "сут.", false, 0.000011574074074, 0, "Сутки (d, сут)"));

            #endregion

            #region Percentage PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Percentage, MeasureCode.Percentage_percent,
                "%", true, 1, 0, "Процент"));

            Measures.Add(new Measure(MeasureGroupCode.Percentage, MeasureCode.Percentage_parts,
                "доли", false, 0.01, 0, "Часть"));

            #endregion

            #region Temperature PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Temperature, MeasureCode.Temperature_Celsium,
                "С", true, 1, 0, "Цельсий"));
            Measures.Add(new Measure(MeasureGroupCode.Temperature, MeasureCode.Temperature_Kelvin,
                "K", false, 1, 273, "Кельвин"));
            Measures.Add(new Measure(MeasureGroupCode.Temperature, MeasureCode.Temperature_Fahrenheit,
                "F", false, 9.0/5, 32, "Фаренгейт"));

            #endregion

            #region Angle PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Angle, MeasureCode.Angle_Degrees,
                "град.", true, 1, 0, "Градусы"));
            Measures.Add(new Measure(MeasureGroupCode.Angle, MeasureCode.Angle_Radian,
                "рад.", false, Math.PI/180, 0, "Радианы"));

            #endregion

            #region Empty PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Empty, MeasureCode.Empty_Just,
                "", true, 1, 0, ""));
            Measures.Add(new Measure(MeasureGroupCode.Empty, MeasureCode.Empty_PerOneThousandth,
                "", false, 1, 0, ""));

            #endregion

            #region ResidualGOR PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.ResidualGOR, MeasureCode.ResidualGor_CubePerCube,
                "м3/м3", true, 1, 0, "Остаточный фактор в м3/м3"));

            #endregion

            #region ResidualWс PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.ResidualWC, MeasureCode.ResidualWC_Percent,
                "%", true, 1, 0, "Остаточный фактор в %"));

            #endregion

            #region Performance PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Performance, MeasureCode.Performance_CubeMeterPerDay,
                "m3/сут", true, 1, 0, "кубометр в сутки"));

            #endregion

            #region Price PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Price, MeasureCode.Price_ThousandthRub,
                "тыс", true, 1, 0, "тыс руб"));

            #endregion

            #region Velocity PercentMeasures

            Measures.Add(new Measure(MeasureGroupCode.Velocity, MeasureCode.Velocity_m_sec,
                "м/с", true, 1, 0, "метры в секунду"));
            Measures.Add(new Measure(MeasureGroupCode.Velocity, MeasureCode.Velocity_k_h,
                "км/ч", false, 3.6, 0, "км/ч"));

            #endregion

            #region PowerEnergetics

            Measures.Add(new Measure(MeasureGroupCode.PowerEnergetics, MeasureCode.PowerEnergetics_mWt,
                "МВт", true, 1, 0, "мегаватты"));
            Measures.Add(new Measure(MeasureGroupCode.PowerEnergetics, MeasureCode.PowerEnergetics_kWt,
                "КВт", false, 1000, 0, "киловатты"));

            #endregion

            #region Density

            Measures.Add(new Measure(MeasureGroupCode.Density, MeasureCode.Density_kg_m3,
                "кг/м3", true, 1, 0, "килограммы на метр кубический"));
            Measures.Add(new Measure(MeasureGroupCode.Density, MeasureCode.Density_t_m3,
                "т/м3", false, 0.001, 0, "тонны на метр кубический"));

            #endregion

            #region DynamicViscosity

            Measures.Add(new Measure(MeasureGroupCode.DynamicViscosity, MeasureCode.DynamicViscosity_PaS,
                "Па*с", true, 1, 0, "паскаль-секунды"));
            Measures.Add(new Measure(MeasureGroupCode.DynamicViscosity, MeasureCode.DynamicViscosity_P,
                "П", false, 0.1, 0, "пуазы"));
            Measures.Add(new Measure(MeasureGroupCode.DynamicViscosity, MeasureCode.DynamicViscosity_sP,
                "сП", false, 0.001, 0, "сантиПуазы"));

            #endregion

            #region CompressibilityFactor

            Measures.Add(new Measure(MeasureGroupCode.CompressibilityFactor, MeasureCode.CompressibilityFactor_1_atm,
                "1/атм", true, 1, 0, "1/атм"));

            #endregion

            #region SurfaceTension

            Measures.Add(new Measure(MeasureGroupCode.SurfaceTension, MeasureCode.SurfaceTension_dim_sm,
                "дин/см", true, 1, 0, "дин/см"));
            Measures.Add(new Measure(MeasureGroupCode.SurfaceTension, MeasureCode.SurfaceTension_N_m,
                "Н/м", false, 1000, 0, "Н/м"));

            #endregion

            #region HeightUndulation

            Measures.Add(new Measure(MeasureGroupCode.HeightUndulation, MeasureCode.HeightUndulation_1_1000,
                "/1000", true, 1, 0, "/1000"));

            #endregion

            #region HeatTransfer

            Measures.Add(new Measure(MeasureGroupCode.HeatTransfer, MeasureCode.HeatTransfer_watt_m2_K,
                "Вт/м2*K", true, 1, 0, "Вт/м2*K"));
            Measures.Add(new Measure(MeasureGroupCode.HeatTransfer, MeasureCode.HeatTransfer_kkal_h_m2_K,
                "ккал/ч*м2*С", false, 0.859845228, 0, "ккал/ч*м2*С"));

            #endregion

            #region HeatConductivity

            Measures.Add(new Measure(MeasureGroupCode.HeatConductivity, MeasureCode.HeatConductivity_watt_m_K,
                "Вт/м*K", true, 1, 0, "Вт/м*K"));
            Measures.Add(new Measure(MeasureGroupCode.HeatConductivity, MeasureCode.HeatConductivity_kkal_h_m_C,
                "ккал/ч*м*С", false, 0.859845228, 0, "ккал/ч*м*С"));
            Measures.Add(new Measure(MeasureGroupCode.HeatConductivity, MeasureCode.HeatConductivity_kal_s_sm_C,
                "кал/с*см*С", false, 0.002388459, 0, "кал/с*см*С"));

            #endregion

            #region Area

            Measures.Add(new Measure(MeasureGroupCode.Area, MeasureCode.Area_m2,
                "м2", true, 1, 0, "м2"));
            Measures.Add(new Measure(MeasureGroupCode.Area, MeasureCode.Area_mm2,
                "мм2", false, 1e6, 0, "мм2"));

            #endregion

            #region Voltage

            Measures.Add(new Measure(MeasureGroupCode.Voltage, MeasureCode.Voltage_v,
                "В", true, 1, 0, "В"));
   
            #endregion

            #region VolumeToEnergeticsPowerCoeff

            Measures.Add(new Measure(MeasureGroupCode.VolumeToEnergeticsPowerCoeff, MeasureCode.VolumeToEnergeticsPowerCoeff_kWth_m3,
                "кВтч/м3", true, 1, 0, "кВтч/м3"));

            #endregion
        }

        #endregion

        #region Properties

        /// <summary>
        /// Список размерностей
        /// </summary>
        public IList<Measure> Measures { get; protected set; }

        /// <summary>
        /// Размерности в группах
        /// </summary>
        public IDictionary<MeasureGroupCode, IEnumerable<Measure>> GroupsToMeasures
        {
            get
            {
                return _groupsToMeasures ??
                       (_groupsToMeasures = Measures.GroupBy(r => r.MeasureGroupCode)
                                                    .ToDictionary(r => r.Key, e => e.AsEnumerable()));
            }
        }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Возвращает список размерностей по коду группы
        /// </summary>
        public IList<Measure> GetMeasures(MeasureGroupCode groupCode)
        {
            return GroupsToMeasures[groupCode].ToList();
        }

        /// <summary>
        /// Возвращает размерность по коду группы и коду размерности
        /// </summary>
        public Measure GetMeasure(MeasureGroupCode measureGroupCode, MeasureCode measureCode)
        {
            return Measures.Single(r => (r.MeasureGroupCode == measureGroupCode) && (r.MeasureCode == measureCode));
        }
        
        #endregion

        #region Public static methods

        /// <summary>
        /// Конверирует значение из одной размерности в другую
        /// </summary>
        public static double ConvertMeasure(Measure sourceMeasure, Measure destMeasure, double convertValue)
        {
            if (destMeasure == null)
                throw new ArgumentException();

            if (sourceMeasure == null)
                throw new ArgumentException();

            var valueSi = (convertValue - sourceMeasure.ConvertValueB) / sourceMeasure.ConvertValueK;
            var converted = valueSi * destMeasure.ConvertValueK + destMeasure.ConvertValueB;

            return converted;
        }

        #endregion
    }
}

namespace WpfSamples40.ComplexSamples.Measures
{
    /// <summary>
    /// Коды доступных размерностей единиц измерений
    /// </summary>
    public enum MeasureCode
    {
        /// <summary>
        /// Длина в метрах
        /// </summary>
        Length_Meter,
        /// <summary>
        /// Длина в милиметрах
        /// </summary>
        Length_Millimeter,
        /// <summary>
        /// Длина в сантиметрах
        /// </summary>
        Length_Centimeter,
        /// <summary>
        /// Длина в километрах
        /// </summary>
        Length_Kilometre,
        /// <summary>
        /// Длина в милях
        /// </summary>
        Length_Mile,

        /// <summary>
        /// Объём в м3
        /// </summary>
        Volume_m3,
        /// <summary>
        /// Объём в л
        /// </summary>
        Volume_l,
        
        /// <summary>
        /// Время в секундах
        /// </summary>
        Time_s,
        /// <summary>
        /// Время в минутах
        /// </summary>
        Time_m,
        /// <summary>
        /// Время в часах
        /// </summary>
        Time_h,
        /// <summary>
        /// Время в сутках
        /// </summary>
        Time_d,
        
        /// <summary>
        /// Угол в градусах
        /// </summary>
        Angle_Degrees,
        /// <summary>
        /// Угол в радианах
        /// </summary>
        Angle_Radian,
        
        /// <summary>
        /// Температура в цельсиях
        /// </summary>
        Temperature_Celsium,
        /// <summary>
        /// Температура в кельвинах
        /// </summary>
        Temperature_Kelvin,
        /// <summary>
        /// Температура в фаренгейтах
        /// </summary>
        Temperature_Fahrenheit,

        /// <summary>
        /// Произвольное значение
        /// </summary>
        Empty_Just,
        /// <summary>
        /// Произвольное значение что-то /1000
        /// </summary>
        Empty_PerOneThousandth,

        /// <summary>
        /// Цена в тысячах
        /// </summary>
        Price_ThousandthRub,
        
        /// <summary>
        /// Давление в атмосферах
        /// </summary>
        Pressure_Atmosphere,
        /// <summary>
        /// Давление в технических атмосферах
        /// </summary>
        Pressure_TechAtmosphere,
        /// <summary>
        /// Давление в паскалях
        /// </summary>
        Pressure_Pascal,
        /// <summary>
        /// Давление в барах
        /// </summary>
        Pressure_Bar,

        /// <summary>
        /// Производительность в кубометрах за сутки
        /// </summary>
        Performance_CubeMeterPerDay,
        
        /// <summary>
        /// Остаточный фактор m3/m3
        /// </summary>
        ResidualGor_CubePerCube,
        
        /// <summary>
        /// Остаточный фактор в процентах
        /// </summary>
        ResidualWC_Percent,
        
        /// <summary>
        /// Проценты в процентах
        /// </summary>
        Percentage_percent,
        /// <summary>
        /// Проценты в долях
        /// </summary>
        Percentage_parts,

        /// <summary>
        /// Скорость в метрах в секунду
        /// </summary>
        Velocity_m_sec,
        /// <summary>
        /// Скорость в километрах в час
        /// </summary>
        Velocity_k_h,

        /// <summary>
        /// Мощность по энергетике в мегаваттах
        /// </summary>
        PowerEnergetics_mWt,
        /// <summary>
        /// Мощность по энергетике в киловаттах
        /// </summary>
        PowerEnergetics_kWt,

        /// <summary>
        /// Напряжение в вольтах
        /// </summary>
        Voltage_v,

        /// <summary>
        /// Плотность в кг/м3
        /// </summary>
        Density_kg_m3,
        /// <summary>
        /// Плотность в т/м3
        /// </summary>
        Density_t_m3,

        /// <summary>
        /// Динамическая вязкость, паскаль-секунды
        /// </summary>
        DynamicViscosity_PaS,
        /// <summary>
        /// Динамическая вязкость, пуазы
        /// </summary>
        DynamicViscosity_P,
        /// <summary>
        /// Динамическая вязкость, сантиПуазы
        /// </summary>
        DynamicViscosity_sP,

        /// <summary>
        /// Сжимаемость, 1/атм
        /// </summary>
        CompressibilityFactor_1_atm,

        /// <summary>
        /// Коэффициент поверхностного натяжения, Н/м
        /// </summary>
        SurfaceTension_N_m,
        /// <summary>
        /// Коэффициент поверхностного натяжения, дин/см
        /// </summary>
        SurfaceTension_dim_sm,

        /// <summary>
        /// Количество неровностей, 1/1000
        /// </summary>
        HeightUndulation_1_1000,

        /// <summary>
        /// Коэффициент теплоотдачи, Вт/(м2*К)
        /// </summary>
        HeatTransfer_watt_m2_K,
        /// <summary>
        /// Коэффициент теплоотдачи, ккал/(ч*м2*К)
        /// </summary>
        HeatTransfer_kkal_h_m2_K,

        /// <summary>
        /// Коэффициент теплопроводности, Вт/(м*К)
        /// </summary>
        HeatConductivity_watt_m_K,
        /// <summary>
        /// Коэффициент теплопроводности, кал/(c*м*С)
        /// </summary>
        HeatConductivity_kal_s_sm_C,
        /// <summary>
        /// Коэффициент теплопроводности, ккал/(ч*м*С)
        /// </summary>
        HeatConductivity_kkal_h_m_C,
        /// <summary>
        /// Площадь, м2
        /// </summary>
        Area_m2,
        /// <summary>
        /// Площадь, мм2
        /// </summary>
        Area_mm2,
        /// <summary>
        /// Коэффициент W/Q, кВтч/м3
        /// </summary>
        VolumeToEnergeticsPowerCoeff_kWth_m3
    }
}

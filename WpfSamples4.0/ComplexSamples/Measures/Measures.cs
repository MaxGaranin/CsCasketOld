namespace WpfSamples40.ComplexSamples.Measures
{
    public class Measures
    {
        //TODO: Сюда надо, по-хорошему, добавить все размерности (пока здесь только используемые)

        // Length
        public static Measure LengthM
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.Length, MeasureCode.Length_Meter); }
        }

        public static Measure LengthMm
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.Length, MeasureCode.Length_Millimeter); }
        }

        // Temperature
        public static Measure TemperatureC
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.Temperature, MeasureCode.Temperature_Celsium); }
        }

        public static Measure TemperatureK
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.Temperature, MeasureCode.Temperature_Kelvin); }
        }

        public static Measure TemperatureF
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.Temperature, MeasureCode.Temperature_Fahrenheit); }
        }

        // Viscosity
        public static Measure ViscositySp
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.DynamicViscosity, MeasureCode.DynamicViscosity_sP); }
        }

        public static Measure ViscosityP
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.DynamicViscosity, MeasureCode.DynamicViscosity_P); }
        }

        public static Measure ViscosityPaS
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.DynamicViscosity, MeasureCode.DynamicViscosity_PaS); }
        }


        // HeightUndulation
        public static Measure HeightUndulation1K
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.HeightUndulation, MeasureCode.HeightUndulation_1_1000); }
        }

        // HeatTransfer
        public static Measure HeatTransferWm2K
        {
            get { return MeasureManager.Instance.GetMeasure(MeasureGroupCode.HeatTransfer, MeasureCode.HeatTransfer_watt_m2_K); }
        }

    }
}
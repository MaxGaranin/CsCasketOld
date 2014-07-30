using System.Collections.Generic;

namespace WpfSamples40.ComplexSamples.Measures
{
    public class MeasureGroups
    {
        public static IEnumerable<Measure> LengthMeasures
        {
            get { return MeasureManager.Instance.GetMeasures(MeasureGroupCode.Length); }
        }

        public static IEnumerable<Measure> TemperatureMeasures
        {
            get { return MeasureManager.Instance.GetMeasures(MeasureGroupCode.Temperature); }
        }

        public static IEnumerable<Measure> ViscosityMeasures
        {
            get { return MeasureManager.Instance.GetMeasures(MeasureGroupCode.DynamicViscosity); }
        }

        public static IEnumerable<Measure> PressureMeasures
        {
            get { return MeasureManager.Instance.GetMeasures(MeasureGroupCode.Pressure); }
        }

        public static IEnumerable<Measure> HeatTransferMeasures
        {
            get { return MeasureManager.Instance.GetMeasures(MeasureGroupCode.HeatTransfer); }
        }

        public static IEnumerable<Measure> HeightUndulationMeasures
        {
            get { return MeasureManager.Instance.GetMeasures(MeasureGroupCode.HeightUndulation); }
        }

    }
}
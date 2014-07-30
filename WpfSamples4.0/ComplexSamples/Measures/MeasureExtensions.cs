using System.Collections.Generic;
using System.Linq;

namespace WpfSamples40.ComplexSamples.Measures
{
    //TODO: Явный костыль, надо создавать класс групп размерностей
    public static class MeasureExtensions
    {
        /// <summary>
        /// Возвращает список размерностей для группы
        /// </summary>
        public static IEnumerable<Measure> GetMeasures(this MeasureGroupCode groupCode)
        {
            return MeasureManager.Instance.Measures
                .Where(m => m.MeasureGroupCode == groupCode);
        }

        /// <summary>
        /// Возвращает название группы по коду
        /// </summary>
        public static string GetGroupName(this MeasureGroupCode groupCode)
        {
            switch (groupCode)
            {
                case MeasureGroupCode.Angle:
                    return "Угол";
                case MeasureGroupCode.Length:
                    return "Длина";
                case MeasureGroupCode.Time:
                    return "Время";
                case MeasureGroupCode.Volume:
                    return "Объём";
                case MeasureGroupCode.Temperature:
                    return "Температура";
                case MeasureGroupCode.Price:
                    return "Цена";
                case MeasureGroupCode.Pressure:
                    return "Давление";
                case MeasureGroupCode.Performance:
                    return "Производительность";
                case MeasureGroupCode.ResidualGOR:
                    return "ГНФ/ГЖФ";
                case MeasureGroupCode.Power:
                    return "Мощность";
                case MeasureGroupCode.PowerEnergetics:
                    return "Мощность по энергетике";
                case MeasureGroupCode.ResidualWC:
                    return "ВГФ/ЖГФ";
                case MeasureGroupCode.Percentage:
                    return "Процент";
                case MeasureGroupCode.Velocity:
                    return "Скорость";
                case MeasureGroupCode.Density:
                    return "Плотность";
                case MeasureGroupCode.DynamicViscosity:
                    return "Дин. вязкость";
                case MeasureGroupCode.CompressibilityFactor:
                    return "Сжимаемость";
                case MeasureGroupCode.SurfaceTension:
                    return "Коэф. поверхностного натяжения";
                case MeasureGroupCode.HeightUndulation:
                    return "Количество неровностей";
                case MeasureGroupCode.HeatTransfer:
                    return "Коэф. теплоотдачи";
                case MeasureGroupCode.HeatConductivity:
                    return "Коэф. теплопроводности";
                case MeasureGroupCode.Area:
                    return "Площадь";
            }
            return "";
        }
    }
}
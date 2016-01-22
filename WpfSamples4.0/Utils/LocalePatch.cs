using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace WpfSamples40.Utils
{
    /// <summary>
    /// Патч, который исправляет баг WPF, из-за которого игнорируются настройки локали, измененные в панели управления.
    /// </summary>
    public static class LocalePatch
    {
        static LocalePatch()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            XmlLanguage lang = XmlLanguage.GetLanguage(currentCulture.Name);
            lang.GetEquivalentCulture();
            lang.GetSpecificCulture();

            Type langType = typeof(XmlLanguage);
            BindingFlags accessFlags =
                BindingFlags.ExactBinding | BindingFlags.SetField |
                BindingFlags.Instance | BindingFlags.NonPublic;

            FieldInfo field;
            field = langType.GetField("_equivalentCulture", accessFlags);
            field.SetValue(lang, currentCulture);
            field = langType.GetField("_specificCulture", accessFlags);
            field.SetValue(lang, currentCulture);
            field = langType.GetField("_compatibleCulture", accessFlags);
            field.SetValue(lang, currentCulture);

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement), new FrameworkPropertyMetadata(lang));
        }

        /// <summary>
        /// Применить патч.
        /// </summary>
        /// <remarks>
        public static void Init()
        {
        }
    }
}
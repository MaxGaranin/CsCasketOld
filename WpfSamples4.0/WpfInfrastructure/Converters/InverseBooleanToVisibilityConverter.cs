using System.Windows;

namespace WpfSamples40.WpfInfrastructure.Converters
{
    public class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible) { }
    }
}
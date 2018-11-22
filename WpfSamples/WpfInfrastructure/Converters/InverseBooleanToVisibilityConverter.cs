using System.Windows;

namespace WpfSamples.WpfInfrastructure.Converters
{
    public class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible) { }
    }
}
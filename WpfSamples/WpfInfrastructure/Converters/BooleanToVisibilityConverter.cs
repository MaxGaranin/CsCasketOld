using System.Windows;

namespace WpfSamples.WpfInfrastructure.Converters
{
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed) { }
    }
}
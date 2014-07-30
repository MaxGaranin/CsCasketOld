using System.Windows;
using System.Windows.Shapes;
using WpfSamples40.ComplexSamples.GraphEditor.Domain;

namespace WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements
{
    public class WellViewModel : ElementViewModel
    {
        public WellViewModel()
        {
            DomainObject = new DomainObject()
            {
                Name = "Скважина " + GraphEditorApp.Instance.GetNewElementIndex()
            };

            var ellipse = new Ellipse();
            ellipse.Style = (Style) Application.Current.Resources["WellShape"];
            Shape = ellipse;
            Shape.DataContext = this;
        }
    }
}
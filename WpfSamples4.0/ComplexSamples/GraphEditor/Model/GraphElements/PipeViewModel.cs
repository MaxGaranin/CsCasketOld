using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfSamples40.ComplexSamples.GraphEditor.Domain;

namespace WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements
{
    public class PipeViewModel : ElementViewModel
    {
        public PipeViewModel()
        {
            DomainObject = new DomainObject()
            {
                Name = "Труба " + GraphEditorApp.Instance.GetNewElementIndex()
            };

            Shape line = new Line();
            line.Style = (Style) Application.Current.Resources["PipeShape"];
            Shape = line;
            Shape.DataContext = this;
        }
    }
}
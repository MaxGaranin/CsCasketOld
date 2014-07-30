using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfSamples40.ComplexSamples.GraphEditor.Domain;

namespace WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements
{
    public class UpsvViewModel : ElementViewModel
    {
        public UpsvViewModel()
        {
            DomainObject = new DomainObject()
            {
                Name = "УПСВ " + GraphEditorApp.Instance.GetNewElementIndex()
            };
            
            Shape rectangle = new Rectangle();
            rectangle.Style = (Style) Application.Current.Resources["UpsvShape"];
            Shape = rectangle;
            Shape.DataContext = this;
        }

    }
}
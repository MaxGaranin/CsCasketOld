using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfSamples.View.Draw
{
    public partial class DrawWellsMapView : Window
    {
        private class DrawObject
        {
            public Shape Shape { get; set; }
        }

        private IList<WellPoint> _points = new List<WellPoint>(); 
        private IDictionary<WellPoint, DrawObject>  _drawObjects = new Dictionary<WellPoint, DrawObject>();

        private double _xMin;
        private double _xMax;
        private double _yMin;
        private double _yMax;

        private bool _fMove;
        private double _dx;
        private double _dy;

        public DrawWellsMapView()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
                {
                    ReadWellPoints();
                    Draw();
                };
        }

        private void ReadWellPoints()
        {
            _points = DataReader.ReadWells(@"Draw\B2-HO.txt");
            _xMin = _points.Min(p => p.X);
            _xMax = _points.Max(p => p.X);
            _yMin = _points.Min(p => p.Y);
            _yMax = _points.Max(p => p.Y);

            foreach (var point in _points)
            {
                var ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.Stroke = new SolidColorBrush(Colors.Black);
                drawCanvas.Children.Add(ellipse);

                _drawObjects.Add(point, new DrawObject()
                    {
                        Shape = ellipse
                    });
            }
        }

        private void Draw()
        {
            foreach (var point in _points)
            {
                var ellipse = _drawObjects[point].Shape;
                var x = (point.X - _xMin) * drawCanvas.ActualWidth / (_xMax - _xMin);
                var y = drawCanvas.ActualHeight - (point.Y - _yMin) * drawCanvas.ActualHeight / (_yMax - _yMin);
                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);
            }
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
//            if (_fMove)
//            {
//                msCoord.dX = msCoord.dX - curCoord.X + e.X;
//                msCoord.dY = msCoord.dY - curCoord.Y + e.Y;
//            }

            var pos = e.GetPosition(drawCanvas);
            txtCoords.Text = string.Format("{0}:{1}", pos.X, pos.Y);
        }

        private void drawCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }

        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _fMove = true;
        }

        private void drawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _fMove = false;
        }
    }
}

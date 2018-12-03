using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        private CancellationTokenSource _cancellationTokenSource;
        private IList<WellPoint> _points = new List<WellPoint>();
        private IDictionary<WellPoint, DrawObject> _drawObjects = new Dictionary<WellPoint, DrawObject>();

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

            _cancellationTokenSource = new CancellationTokenSource();

            Loaded += (sender, args) =>
            {
                ReadWellPoints();
                Draw();
            };

            Closing += OnClosing;
        }

        private void ReadWellPoints()
        {
            _points = DataReader.ReadWells(@".\..\..\View\Draw\B2-HO.txt");
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

        private void AddMovingEllipseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => AddMovingEllipse(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        private void AddMovingEllipse(CancellationToken cancellationToken)
        {
            Ellipse ellipse = null;
            var diam = 30;
            var step = 5;
            var rnd = new Random();

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var randomColor = Color.FromArgb(255, (byte) rnd.Next(256), (byte) rnd.Next(256), (byte) rnd.Next(256));

                ellipse = new Ellipse
                {
                    Width = diam,
                    Height = diam,
                    Stroke = new SolidColorBrush(randomColor), 
                    Fill = new SolidColorBrush(randomColor)
                };

                drawCanvas.Children.Add(ellipse);
            });

            int x = (int) (rnd.NextDouble() * drawCanvas.ActualHeight);
            int y = (int) (rnd.NextDouble() * drawCanvas.ActualWidth);
            var dx = step;
            var dy = step;

            for (int i = 0; i < 1000; i++)
            {
                if ((x - diam / 2) <= 0 || (x + diam / 2) >= drawCanvas.ActualWidth)
                {
                    dx = -dx;
                }

                if ((y - diam / 2) <= 0 || (y + diam / 2) >= drawCanvas.ActualHeight)
                {
                    dy = -dy;
                }

                x += dx;
                y += dy;

                cancellationToken.ThrowIfCancellationRequested();

                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Canvas.SetLeft(ellipse, x);
                    Canvas.SetTop(ellipse, y);
                });

                Thread.Sleep(20);
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            CancelAllTasks();
        }

        private void CancelTasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            CancelAllTasks();
        }

        private void CancelAllTasks()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
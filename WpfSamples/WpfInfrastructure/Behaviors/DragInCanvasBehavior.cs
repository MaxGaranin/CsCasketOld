using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace WpfSamples40.WpfInfrastructure.Behaviors
{
    public class DragInCanvasBehavior : Behavior<FrameworkElement>
    {
        private Canvas _canvas;

        // Отслеживание перетаскивания элемента
        private bool _isDragging = false;

        // Запись точной позиции, в которой нажата кнопка
        private Point _mouseOffset;

        protected override void OnAttached()
        {
            base.OnAttached();

            // Присоединение обработчиков событий            
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // Удаление обработчиков событий
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Поиск canvas
            if (_canvas == null) _canvas = VisualTreeHelper.GetParent(this.AssociatedObject) as Canvas;

            // Режим перетаскивания
            _isDragging = true;

            // Получение позиции нажатия относительно элемента
            _mouseOffset = e.GetPosition(AssociatedObject);

            // Захват мыши
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Получение позиции элемента относительно Canvas
                Point point = e.GetPosition(_canvas);

                // Move the element.
                AssociatedObject.SetValue(Canvas.TopProperty, point.Y - _mouseOffset.Y);
                AssociatedObject.SetValue(Canvas.LeftProperty, point.X - _mouseOffset.X);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                AssociatedObject.ReleaseMouseCapture();
                _isDragging = false;
            }
        }         
    }
}
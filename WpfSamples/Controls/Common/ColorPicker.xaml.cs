using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfSamples.Controls.Common
{
    public partial class ColorPicker : UserControl
    {
        private Color? _previousColor;

        public ColorPicker()
        {
            InitializeComponent();
            SetupCommands();
        }

        static ColorPicker()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof (Color), typeof (ColorPicker),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof (byte), typeof (ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRgbChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof (byte), typeof (ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRgbChanged)));
            BlueProperty = DependencyProperty.Register("Blue", typeof (byte), typeof (ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRgbChanged)));
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
                typeof (RoutedPropertyChangedEventHandler<Color>), typeof (ColorPicker));
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorpicker = (ColorPicker) d;
            colorpicker._previousColor = (Color) e.OldValue;

            var newColor = (Color) e.NewValue;
            colorpicker.Red = newColor.R;
            colorpicker.Green = newColor.G;
            colorpicker.Blue = newColor.B;
        }

        private static void OnColorRgbChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker) d;
            var color = colorPicker.Color;

            if (e.Property == RedProperty)
            {
                color.R = (byte) e.NewValue;
            }
            else if (e.Property == GreenProperty)
            {
                color.G = (byte) e.NewValue;
            }
            else if (e.Property == BlueProperty)
            {
                color.B = (byte) e.NewValue;
            }

            colorPicker.Color = color;
        }

        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;
        public static readonly RoutedEvent ColorChangedEvent;

        public Color Color
        {
            get { return (Color) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public byte Red
        {
            get { return (byte) GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Green
        {
            get { return (byte) GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public byte Blue
        {
            get { return (byte) GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        private void SetupCommands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(ColorPicker),
                new CommandBinding(ApplicationCommands.Undo, UndoCommand_Executed, UndoCommand_CanExecute));
        }

        private static void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var colorPicker = (ColorPicker)sender;
            colorPicker.Color = (Color) colorPicker._previousColor;
        }

        private static void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var colorPicker = (ColorPicker) sender;
            e.CanExecute = colorPicker._previousColor.HasValue;
        }
    }
}
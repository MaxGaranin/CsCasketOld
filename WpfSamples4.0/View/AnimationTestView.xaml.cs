using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfSamples40.View
{
    public partial class AnimationTestView : Window
    {
        public AnimationTestView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            DoubleAnimation anim = new DoubleAnimation
            {
                From = button.Width,
                To = this.Width - 20,
                Duration = TimeSpan.FromSeconds(5)
            };

            button.BeginAnimation(Button.WidthProperty, anim);
        }
    }
}
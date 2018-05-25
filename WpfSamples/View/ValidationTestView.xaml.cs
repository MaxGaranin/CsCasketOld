using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfSamples40.View
{
    public partial class ValidationTestView : Window
    {
        public ValidationTestView()
        {
            InitializeComponent();

            TestObservables();
        }

        private void TestObservables()
        {
            var eventsAsObservable = (
                    from move in Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseDown")
                    let pos = move.EventArgs.GetPosition(this)
                    select new { pos.X, pos.Y }
                )
                .TimeInterval()
                .Where(e => e.Interval.TotalMilliseconds < 500);

            eventsAsObservable.Subscribe(e =>
                Console.WriteLine(@"Double click: X={0}, Y={1}, Interval={2}",
                    e.Value.X, e.Value.Y, e.Interval));
        }
    }
}
using System;
using System.Reactive.Linq;

namespace ConsoleApp.Observables
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IObservable<int> range =
                from i in Observable.Range(1, 10)
                where i % 2 == 0
                select i;

            range.Subscribe(
                i => Console.WriteLine("Next element: {0}", i),
                e => Console.WriteLine("Error: {0}", e.Message),
                () => Console.WriteLine("Range observation complete")
            );

            Console.ReadLine();
        }
    }
}
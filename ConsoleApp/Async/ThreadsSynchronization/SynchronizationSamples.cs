using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.ThreadsSynchronization
{
    public class SynchronizationSamples
    {
        private static int _val1, _val2;

        public static void Main(string[] args)
        {
            var t1 = Task.Run(() => { Go(); });
            var t2 = Task.Run(() => { Go(); });

            Task.WaitAll(t1, t2);
            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void Go()
        {
            lock (typeof(SynchronizationSamples))
            {
                if (_val2 != 0)
                {
                    Console.WriteLine(_val1 / _val2);
                }

                _val2 = 0;
                
                Thread.Sleep(2000);
            }
        }
    }
}
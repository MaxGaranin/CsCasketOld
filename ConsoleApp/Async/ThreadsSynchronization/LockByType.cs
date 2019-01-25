using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.ThreadsSynchronization
{
    public class LockByType
    {
        private static int _val1, _val2;

        public static void Main(string[] args)
        {
            var t1 = Task.Run(() => { Go(1); });
            var t2 = Task.Run(() => { Go(2); });

            Task.WaitAll(t1, t2);
            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void Go(int k)
        {
            lock (typeof(LockByType))
            {
                if (_val2 != 0)
                {
                    Console.WriteLine(_val1 / _val2);
                }

                _val2 = 0;
                
                Console.WriteLine($"Thread: {k}");
                Thread.Sleep(2000);
            }
        }
    }
}
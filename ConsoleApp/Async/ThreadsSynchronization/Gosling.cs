using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.ThreadsSynchronization
{
    public class Gosling
    {
        private static readonly object Lock = new object();
        private static int _counter = 0;

        public static void Main(string[] args)
        {
            var waiterTask = Task.Run(() => Waiter());
            var timerTask = Task.Run(() => Worker());

            Console.ReadKey();
        }

        internal static void Worker()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Прошло секунд: {_counter}");
                _counter++;

                lock (Lock)
                {
                    Monitor.PulseAll(Lock);
                }

                if (_counter > 100) break;
            }
        }

        internal static void Waiter()
        {
            lock (Lock)
            {
                while (true)
                {
                    if (_counter > 0 && _counter % 5 != 0)
                    {
                        Console.WriteLine("Получили сигнал!");
                    }
                    else
                    {
                        Monitor.Wait(Lock);
                    }
                }
            }
        }
    }
}
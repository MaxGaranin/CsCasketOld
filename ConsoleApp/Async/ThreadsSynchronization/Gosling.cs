using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.ThreadsSynchronization
{
    /// <summary>
    /// Задача на многопоточность из книги Гослинга
    /// https://ru.stackoverflow.com/questions/338205/Задача-на-многопоточность-из-книги-Гослинга
    /// </summary>
    public class Gosling
    {
        private static readonly object Lock = new object();
        private static int _counter = 0;
        private static int _end = 15;

        public static void Main(string[] args)
        {
            var waiter5Task = Task.Run(() => Waiter(5));
            var waiter7Task = Task.Run(() => Waiter(7));
            var workerTask = Task.Run(() => Worker());

            Console.ReadKey();
        }

        internal static void Worker()
        {
            while (true)
            {
                Thread.Sleep(1000);
                _counter++;
                Console.WriteLine($"Прошло секунд: {_counter}");

                lock (Lock)
                {
                    Monitor.PulseAll(Lock);
                }

                if (_counter >= _end) break;
            }
        }

        internal static void Waiter(int n)
        {
            lock (Lock)
            {
                while (true)
                {
                    if (_counter > 0 && _counter % n == 0)
                    {
                        Console.WriteLine("Получили сигнал!");
                    }

                    Monitor.Wait(Lock);
                }
            }
        }
    }
}
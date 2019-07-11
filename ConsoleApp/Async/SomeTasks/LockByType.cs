using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.SomeTasks
{
    public class LockByTypeRunner
    {
        public static void Main(string[] args)
        {
            var lockTask = Task.Run(() => { LockByType.OuterLockType(); });
            var goTask = Task.Run(() => { LockByType.Go(1); });

            Console.ReadKey();
        }
    }

    public class LockByType
    {
        private static object _lock = new object();

        public static void Go(int k)
        {
            while (true)
            {
                lock (typeof(LockByType))
                {
                    Console.WriteLine($"Thread: {k}");
                    Thread.Sleep(1000);
                }
            }
        }

        public static void OuterLockType()
        {
            while (true)
            {
                // В этом случае потоки не блокируют друга друга
                // lock (_lock)             

                // Здесь Go() будет ждать, пока не выполнится данный lock
                lock (typeof(LockByType))
                {
                    Console.WriteLine("Lock");
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
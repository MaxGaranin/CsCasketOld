using System;
using System.Threading;

namespace ConsoleApp.Async.Metanit
{
    public class MonitorTickTock
    {
        public class Program
        {
            public static void Main()
            {
                var tt = new MonitorTickTock();
                var mt1 = new MyThread("Tick", tt);
                var mt2 = new MyThread("Tock", tt);
                mt1.Thread.Join();
                mt2.Thread.Join();

                Console.WriteLine("Часы остановлены");
                Console.ReadLine();
            }
        }

        private readonly object _lockOn = new object();

        public void Tick(bool running)
        {
            lock (_lockOn)
            {
                if (!running)
                {
                    // Остановить часы
                    Monitor.Pulse(_lockOn);
                    return;
                }

                Console.Write("Тик ");

                // Разрешить выполнение метода Tock()
                Monitor.Pulse(_lockOn);

                // Ожидать завершение Tock()
                Monitor.Wait(_lockOn);
            }
        }

        public void Tock(bool running)
        {
            lock (_lockOn)
            {
                if (!running)
                {
                    Monitor.Pulse(_lockOn);
                    return;
                }

                Console.WriteLine("так");

                Monitor.Pulse(_lockOn);
                Monitor.Wait(_lockOn);
            }
        }
    }

    internal class MyThread
    {
        private readonly MonitorTickTock _tt;

        public Thread Thread { get; set; }

        // Новый поток
        public MyThread(string name, MonitorTickTock tt)
        {
            _tt = tt;

            Thread = new Thread(Run);
            Thread.Name = name;
            Thread.Start();
        }

        public void Run()
        {
            if (Thread.Name == "Tick")
            {
                for (var i = 0; i < 5; i++)
                {
                    _tt.Tick(true);
                }
                _tt.Tick(false);
            }
            else
            {
                for (var i = 0; i < 5; i++)
                {
                    _tt.Tock(true);
                }
                _tt.Tock(false);
            }
        }
    }
}
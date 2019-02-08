using System;
using System.Threading;

namespace ConsoleApp.Async.Metanit
{
    public class MonitorWork
    {
        private static int _x = 0;
        private static object _lock = new object();

        public static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                var myThread = new Thread(Count);
                myThread.Name = "Поток " + i;
                myThread.Start();
            }

            Console.ReadLine();
        }

        public static void Count()
        {
//            lock (_lock)
//            {
                _x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, _x);
                    _x++;
                    Thread.Sleep(100);
                }
//            }
        }
    }
}
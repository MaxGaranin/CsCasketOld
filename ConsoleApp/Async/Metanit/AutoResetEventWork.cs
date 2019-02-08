using System;
using System.Threading;

namespace ConsoleApp.Async.Metanit
{
    public class AutoResetEventWork
    {
        private static AutoResetEvent _waitHandler = new AutoResetEvent(true);
        private static int _x = 0;

        private static void Main(string[] args)
        {
            for (var i = 0; i < 5; i++)
            {
                var myThread = new Thread(Count);
                myThread.Name = "Поток " + i;
                myThread.Start();
            }

            Console.ReadLine();
        }

        public static void Count()
        {
            _waitHandler.WaitOne();
            _x = 1;
            for (var i = 1; i < 9; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, _x);
                _x++;
                Thread.Sleep(100);
            }
            _waitHandler.Set();
        }
    }
}
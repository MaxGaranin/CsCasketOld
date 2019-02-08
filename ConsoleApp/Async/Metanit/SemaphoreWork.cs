using System;
using System.Threading;

namespace ConsoleApp.Async.Metanit
{
    public class SemaphoreWork
    {
        public static void Main(string[] args)
        {
            for (int i = 1; i < 6; i++)
            {
                var reader = new Reader(i);
            }

            Console.ReadLine();
        }

        public class Reader
        {
            // создаем семафор
            private static Semaphore _sem = new Semaphore(3, 3);
            private Thread _myThread;
            private int _count = 3; // счетчик чтения

            public Reader(int i)
            {
                _myThread = new Thread(Read);
                _myThread.Name = "Читатель " + i;
                _myThread.Start();
            }

            public void Read()
            {
                while (_count > 0)
                {
                    _sem.WaitOne();

                    Console.WriteLine("{0} входит в библиотеку", Thread.CurrentThread.Name);

                    Console.WriteLine("{0} читает", Thread.CurrentThread.Name);
                    Thread.Sleep(1000);

                    Console.WriteLine("{0} покидает библиотеку", Thread.CurrentThread.Name);

                    _sem.Release();

                    _count--;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
using System;
using System.Threading;

namespace ConsoleApp.Samples.ThreadSyncWork
{
    public class ManualResetEventWork
    {
        static void Main()
        {
            var evtObj = new ManualResetEvent(false);

            var mt1 = new MyThread("Событийный поток 1", evtObj);

            Console.WriteLine("Основной поток ожидает событие");

            evtObj.WaitOne();

            Console.WriteLine("Основной поток получил уведомление о событии от первого потока");

            evtObj.Reset();

            mt1 = new MyThread("Событийный поток 2", evtObj);

            evtObj.WaitOne();

            Console.WriteLine("Основной поток получил уведомление о событии от второго потока");
            Console.ReadLine();
        }

        class MyThread
        {
            public Thread Thrd;
            ManualResetEvent mre;

            public MyThread(string name, ManualResetEvent evt)
            {
                Thrd = new Thread(this.Run);
                Thrd.Name = name;
                mre = evt;
                Thrd.Start();
            }

            void Run()
            {
                Console.WriteLine("Внутри потока " + Thrd.Name);

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(Thrd.Name);
                    Thread.Sleep(500);
                }

                Console.WriteLine(Thrd.Name + " завершен!");

                // Уведомление о событии
                mre.Set();
            }
        }
    }
}
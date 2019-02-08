using System;
using System.Threading;

namespace ConsoleApp.Async.Metanit
{
    public class MutexIncDec
    {
        public static void Main()
        {
            IncThread mt1 = new IncThread("Inc thread", 5);

            // разрешить инкременирующему потоку начаться
            Thread.Sleep(1);

            DecThread mt2 = new DecThread("Dec thread", 5);

            mt1.Thrd.Join();
            mt2.Thrd.Join();

            Console.ReadLine();
        }

        // В этом классе содержится общий ресурс в виде переменной Count,
        // а так же мьютекс mtx
        public static class SharedRes
        {
            public static int Count;
            public static readonly Mutex Mtx = new Mutex();
        }

        // В этом классе Count инкрементируется
        public class IncThread
        {
            int num;
            public Thread Thrd;

            public IncThread(string name, int n)
            {
                Thrd = new Thread(this.Run);
                num = n;
                Thrd.Name = name;
                Thrd.Start();
            }

            // Точка входа в поток
            public void Run()
            {
                Console.WriteLine(Thrd.Name + " ожидает мьютекс");

                // Получить мьютекс
                SharedRes.Mtx.WaitOne();

                Console.WriteLine(Thrd.Name + " получает мьютекс");

                do
                {
                    Thread.Sleep(500);
                    SharedRes.Count++;
                    Console.WriteLine("в потоке {0}, Count={1}", Thrd.Name, SharedRes.Count);
                    num--;
                } while (num > 0);

                Console.WriteLine(Thrd.Name + " освобождает мьютекс");

                SharedRes.Mtx.ReleaseMutex();
            }
        }

        public class DecThread
        {
            int num;
            public Thread Thrd;

            public DecThread(string name, int n)
            {
                Thrd = new Thread(new ThreadStart(this.Run));
                num = n;
                Thrd.Name = name;
                Thrd.Start();
            }

            // Точка входа в поток
            public void Run()
            {
                Console.WriteLine(Thrd.Name + " ожидает мьютекс");

                // Получить мьютекс
                SharedRes.Mtx.WaitOne();

                Console.WriteLine(Thrd.Name + " получает мьютекс");

                do
                {
                    Thread.Sleep(500);
                    SharedRes.Count--;
                    Console.WriteLine("в потоке {0}, Count={1}", Thrd.Name, SharedRes.Count);
                    num--;
                } while (num > 0);

                Console.WriteLine(Thrd.Name + " освобождает мьютекс");

                SharedRes.Mtx.ReleaseMutex();
            }
        }
    }
}

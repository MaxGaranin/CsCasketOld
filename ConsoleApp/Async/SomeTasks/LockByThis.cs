using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.SomeTasks
{
    public class LockByThisRunner
    {
        public static void Main(string[] args)
        {
            var obj = new LockByThis();

            Task.Run(() =>
            {
                while (true)
                {
                    lock (obj)             
                    {
                        Console.WriteLine("Lock");
                        Thread.Sleep(3000);
                    }
                }
            });

            obj.Go(1);

            Console.ReadKey();
        }
    }

    public class LockByThis
    {
        public void Go(int k)
        {
            while (true)
            {
                lock (this)
                {
                    Console.WriteLine($"Thread: {k}");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
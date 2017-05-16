using System;
using System.Threading;

namespace ConsoleApp.Samples.ThreadSyncWork
{
    public class MonitorWait
    {
        public static void Main(string[] args)
        {
            object sync = new object();
            var thread = new Thread(() =>
            {
                try
                {
                    Work();
                }
                finally
                {
                    lock (sync)
                    {
                        Monitor.PulseAll(sync);
                    }
                }
            });
            thread.Start();

            lock (sync)
            {
                Monitor.Wait(sync);
            }

            Console.WriteLine("test");
            Console.ReadKey();
        }

        private static void Work()
        {
            Thread.Sleep(1000);
        }
    }
}
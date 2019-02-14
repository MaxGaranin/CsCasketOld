using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.SomeTasks
{
    public class ProducerConsumerByMonitor
    {
        private readonly object _lock = new object();
        private int _count;
        private const int MaxCount = 3;

        public ProducerConsumerByMonitor()
        {
            _count = 0;
        }

        public void Work(int i)
        {
            lock (_lock)
            {
                while (_count > MaxCount)
                {
                    Monitor.Wait(_lock);
                    _count++;
                }
            }

            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Обрабатывается потребитель: {i}, поток: {id}...");
            Thread.Sleep(1000);
            Console.WriteLine($"    Готово: {i}");

            lock (_lock)
            {
                _count--;
                Monitor.PulseAll(_lock);
            }
        }

        public static void Main(string[] args)
        {
            var worker = new ProducerConsumerByMonitor();

            var n = 11;
            var tasks = new Task[n];
            for (int i = 0; i < n; i++)
            {
                var k = i + 1;
                tasks[i] = Task.Run(() => worker.Work(k));
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Готово!");
            Console.ReadKey();
        }
    }
}
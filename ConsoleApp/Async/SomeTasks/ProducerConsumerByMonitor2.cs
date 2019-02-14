using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.SomeTasks
{
    public class ProducerConsumerByMonitor2 : IDisposable
    {
        private readonly object _lock = new object();
        private readonly Queue<string> _tasksQueue;
        private readonly Task[] _workers;

        public ProducerConsumerByMonitor2(int workersCount)
        {
            _tasksQueue = new Queue<string>();

            _workers = new Task[workersCount];
            for (int i = 0; i < workersCount; i++)
            {
                _workers[i] = Task.Run(() => { Work(); });
            }
        }

        public void AddTask(string taskName)
        {
            lock (_lock)
            {
                _tasksQueue.Enqueue(taskName);
                Monitor.PulseAll(_lock);
            }
        }

        private void Work()
        {
            while (true)
            {
                string task;

                lock (_lock)
                {
                    while (_tasksQueue.Count == 0)
                    {
                        Monitor.Wait(_lock);
                    }

                    task = _tasksQueue.Dequeue();
                }

                if (task == null) return;

                var id = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"Выполняется: {task}, поток: {id}...");
                Thread.Sleep(1000);
                Console.WriteLine($"    Готово: {task}");
            }
        }

        public void Dispose()
        {
            foreach (var worker in _workers)
            {
                AddTask(null);                
            }

            Task.WaitAll(_workers);
        }

        public static void Main(string[] args)
        {
            using (var worker = new ProducerConsumerByMonitor2(3))
            {
                for (int i = 0; i < 10; i++)
                {
                    var k = i + 1;
                    worker.AddTask($"Задача {k}");
                }
            }

            Console.WriteLine("Готово!");
            Console.ReadKey();
        }
    }
}
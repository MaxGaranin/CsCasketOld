using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.SomeTasks
{
    public class ProducerConsumerBySemaphore
    {
        private readonly Semaphore _semaphore;

        public ProducerConsumerBySemaphore()
        {
            // интересно, что потоков для выполнения создается не больше числа ядер процессора
            // _semaphore = new Semaphore(5, 5);
            _semaphore = new Semaphore(3, 3);
        }

        public void Work(int i)
        {
            _semaphore.WaitOne();
            
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Обрабатывается потребитель: {i}, поток: {id}...");
            Thread.Sleep(1000);
            Console.WriteLine($"    Готово: {i}");

            _semaphore.Release();
        }
        
        public static void Main(string[] args)
        {
            var worker = new ProducerConsumerBySemaphore();

            for (int i = 0; i < 10; i++)
            {
                var k = i + 1;
                Task.Run(() => worker.Work(k));
            }

            Console.ReadKey();
        }
    }
}
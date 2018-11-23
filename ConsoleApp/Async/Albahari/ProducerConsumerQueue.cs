using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp.Async.Albahari
{
    public class ProducerConsumerQueue : IDisposable
    {
        EventWaitHandle wh = new AutoResetEvent(false);
        Thread worker;
        object locker = new object();
        Queue<string> tasks = new Queue<string>();
 
        public ProducerConsumerQueue() 
        {
            worker = new Thread(Work);
            worker.Start();
        }
 
        public void EnqueueTask(string task)
        {
            lock (locker)
                tasks.Enqueue(task);

            wh.Set();
        }
 
        public void Dispose() 
        {
            EnqueueTask(null); // Сигнал Потребителю на завершение
            worker.Join();     // Ожидание завершения Потребителя
            wh.Close();        // Освобождение ресурсов
        }

        private void Work() 
        {
            while (true) 
            {
                string task = null;

                lock (locker)
                {
                    if (tasks.Count > 0) 
                    {
                        task = tasks.Dequeue();
                        if (task == null)
                            return;
                    }
                }

                if (task != null) 
                {
                    Console.WriteLine("Выполняется задача: " + task);
                    Thread.Sleep(1000); // симуляция работы...
                }
                else
                    wh.WaitOne(); // Больше задач нет, ждем сигнала...
            }
        }

        public static void Main() 
        {
            using(ProducerConsumerQueue q = new ProducerConsumerQueue()) 
            {
                q.EnqueueTask("Привет!");

                for (int i = 0; i < 10; i++)
                    q.EnqueueTask("Сообщение " + i);

                q.EnqueueTask("Пока!");
            }

            // Выход из using приводит к вызову Dispose, который ставит
            // в очередь null-задачу и ожидает, пока Потребитель не завершится.

            Console.ReadKey();
        }
    }
}
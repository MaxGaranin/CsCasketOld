using System;
using System.Threading;

namespace ConsoleApp.Async.Albahari
{
    public class AcknowledgedWaitHandle
    {
        static EventWaitHandle ready = new AutoResetEvent(false);
        static EventWaitHandle go = new AutoResetEvent(false);
        static volatile string task;
 
        static void Main() 
        {
            new Thread(Work).Start();
 
            // Сигнализируем рабочему потоку 5 раз
            for (int i = 1; i <= 5; i++) 
            {
                ready.WaitOne();             // Сначала ждем, когда рабочий поток будет готов
                task = "a".PadRight(i, 'h'); // Назначаем задачу
                go.Set();                    // Говорим рабочему потоку, что можно начинать
            }
 
            // Сообщаем о необходимости завершения рабочего потока,
            // используя null-строку
            ready.WaitOne();
            task = null;
            go.Set();

            Console.ReadKey();
        }
 
        static void Work() 
        {
            while (true) 
            {
                ready.Set();  // Сообщаем о готовности
                go.WaitOne(); // Ожидаем сигнала начать...

                if (task == null)
                    return; // Элегантно завершаемся

                Console.WriteLine(task);
            }
        }
    }
}
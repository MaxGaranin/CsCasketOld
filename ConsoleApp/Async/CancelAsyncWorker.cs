using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async
{
    public class CancelAsyncWorker
    {
        private CancellationTokenSource _cts;

        /// <summary>
        /// Вариант без использования оператора async (но тоже асинхронный)
        /// </summary>
        public void StartWithTasks()
        {
            Console.WriteLine("Start.");

            _cts = new CancellationTokenSource();

            Task.Run(() => SomeWork(_cts.Token), _cts.Token)
                .ContinueWith(t =>
                {
                    // При отмене не покажет ничего
                    Console.WriteLine(t.Exception);

                    if (t.IsCanceled)
                    {
                        Console.WriteLine("Canceled!");
                    }
                    else
                    {
                        Console.WriteLine("Done!");
                    }
                });

            // Отменяем операцию через 1 сек
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Cancel();

            Console.ReadLine();
        }

        private void SomeWork(CancellationToken cancellationToken)
        {
//            throw new NullReferenceException();

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                cancellationToken.ThrowIfCancellationRequested();
            }

            Console.WriteLine("Work finished.");
        }

        private void Cancel()
        {
            _cts.Cancel();
        }
    }
}
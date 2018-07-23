using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.Cancelation
{
    public class CancelTaskWorker
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
                    // Console.WriteLine(t.Exception);

                    if (t.IsCanceled)
                    {
                        Console.WriteLine("Canceled!");
                    }
                    else if (t.IsFaulted)
                    {
                        Console.WriteLine("Faulted!");
                    }
                    else
                    {
                        Console.WriteLine("Done!");
                    }
                });

            // Отменяем операцию через 1 сек
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Cancel();

            Console.ReadKey();
        }

        private static void SomeWork(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                cancellationToken.ThrowIfCancellationRequested();
            }

            Console.WriteLine("Work finished.");

//            throw new NullReferenceException();
        }

        private void Cancel()
        {
            _cts.Cancel();
        }
    }
}
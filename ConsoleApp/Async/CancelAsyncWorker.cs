using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async
{
    public class CancelAsyncWorker
    {
        private CancellationTokenSource _cts;

        public void Start()
        {
            Console.WriteLine("Start.");

            _cts = new CancellationTokenSource();

            var task = Task
                .Run(() => SomeWork(_cts.Token), _cts.Token)
                .ContinueWith((t) =>
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

        private int SomeWork(CancellationToken cancellationToken)
        {
            int result = 1;

//            throw new NullReferenceException();

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                cancellationToken.ThrowIfCancellationRequested();
            }

            Console.WriteLine("Work finished.");
            return result;
        }

        private void Cancel()
        {
            _cts.Cancel();
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async
{
    public class CancelAsyncWorker
    {
        public async void Work()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task.Run(() =>
            {
                Thread.Sleep(2000);
                cts.Cancel();
            });

            try
            {
                await Task.Run(() =>
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                    throw new ArgumentException();
                }, token);

            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine("Task Canceled Exception");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument Exception");
            }
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.Cancelation
{
    public class CancelAsyncWorker
    {
        public async void Work()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

#pragma warning disable 4014
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                cts.Cancel();
            }, token);
#pragma warning restore 4014

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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.ThreadsSynchronization
{
    /// <summary>
    /// Псевдомногопоточная задача про садовников
    /// http://www.cyberforum.ru/csharp-beginners/thread902686.html
    /// </summary>
    public class Garden
    {
        public static void Main(string[] args)
        {
            var garder = new Garden(10, 10);
            garder.Work();

            Console.ReadKey();
        }

        private int _m;
        private int _n;
        private int[,] _arr;
        private object _lock = new object();

        public Garden(int n, int m)
        {
            _n = n;
            _m = m;
            _arr = new int[n, m];
        }

        public void Work()
        {
            var task1 = Task.Run(() => { ExecuteGardener1(); });
            var task2 = Task.Run(() => { ExecuteGardener2(); });

            Task.WaitAll(task1, task2);

            Console.WriteLine("Done!");
            Console.WriteLine();

            for (int i = 0; i < _arr.GetLength(0); i++)
            {
                for (int j = 0; j < _arr.GetLength(1); j++)
                {
                    Console.Write($"{_arr[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        private void ExecuteGardener1()
        {
            for (int i = 0; i < _arr.GetLength(0); i++)
            {
                for (int j = 0; j < _arr.GetLength(1); j++)
                {
                    if (_arr[i, j] == 0)
                    {
                        _arr[i, j] = 1;
                        Thread.Sleep(10);
                    }
                }
            }
        }

        private void ExecuteGardener2()
        {
            for (int i = _arr.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = _arr.GetLength(1) - 1; j >= 0 ; j--)
                {
                    if (_arr[i, j] == 0)
                    {
                        _arr[i, j] = 2;
                        Thread.Sleep(20);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.ParallelTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var names = new List<string> {"Мама", "мыла", "раму", "Завтра", "новый", "день", "zuzu", "abarvalg"};

            var counter = 0;
            Parallel.ForEach(names,
                (name, state) =>
                {
                    Interlocked.Exchange(ref counter, counter + 1);
                    Console.WriteLine("Step {0} of {1}", counter, names.Count);
                });

            Console.ReadKey();
        }
    }
}
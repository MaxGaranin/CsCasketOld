using System;
using System.Threading.Tasks;

namespace ConsoleApp.Async.SomeTasks
{
    /// <summary>
    /// Какой из потоков быстрее?
    /// https://javatalks.ru/topics/31694
    /// </summary>
    public class WhoWins
    {
        private int _a = 0;

        public static void Main(string[] args)
        {
            var whoWins = new WhoWins();

            Task.Run(() => { whoWins.Work1(); });
            Task.Run(() => { whoWins.Work2(); });

            Console.ReadKey();
        }

        private void Work1()
        {
            while (true)
            {
                if (_a == 100) break;
                _a++;
            }

            Console.WriteLine($"Work1 wins: a = {_a}");
        }

        private void Work2()
        {
            while (true)
            {
                if (_a == -100) break;
                _a--;
            }

            Console.WriteLine($"Work2 wins: a = {_a}");
        }
    }
}
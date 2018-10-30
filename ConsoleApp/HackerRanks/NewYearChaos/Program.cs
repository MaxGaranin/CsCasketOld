using System;

namespace ConsoleApp.HackerRanks.NewYearChaos
{
    public class Program
    {
        private static void MinimumBribes(int[] q)
        {
            var k = 0;
            var prevDiff = 0;
            for (var i = 0; i < q.Length; i++)
            {
                var diff = q[i] - (i + 1);
                if (diff > 2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }

                if (diff > 0)
                {
                    k += diff;
                }

                if (i > 0 && diff < 0 && prevDiff < 0 && prevDiff > diff)
                {
                    k++;
                }

                prevDiff = diff;
            }

            Console.WriteLine(k);
        }

        public static void Main()
        {
            var s = "2 1 5 3 4";
            int[] q = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
            MinimumBribes(q);

            s = "2 5 1 3 4";
            q = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
            MinimumBribes(q);

            Console.ReadKey();
        }
    }
}
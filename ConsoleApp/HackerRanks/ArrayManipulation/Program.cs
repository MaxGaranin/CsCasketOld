using System;
using System.IO;
using System.Linq;

namespace ConsoleApp.HackerRanks.ArrayManipulation
{
    public class Program
    {
        private static long ArrayManipulation(int n, int[][] queries) {

            var arr = new long[n + 1];
            var max = long.MinValue;

            foreach (var query in queries) {
                var a = query[0];
                var b = query[1];
                var k = query[2];

                for (var i = a; i <= b; i++) {

                    if (arr[i] > int.MaxValue - k)
                    {

                    }
                    arr[i] += k;
                    if (arr[i] > max) max = arr[i];
                }
            }

            return max;
        }

        public static void Main()
        {
            var sr = new StreamReader(@".\..\..\HackerRanks\ArrayManipulation\input07.txt");
            var s = sr.ReadLine();
            var tokens = s.Split(' ');

            var n = Convert.ToInt32(tokens[0]);
            var m = Convert.ToInt32(tokens[1]);

            var queries = new int[m][];

            for (int i = 0; i < m; i++)
            {
                var line = sr.ReadLine();
                if (string.IsNullOrEmpty(line)) break;

                queries[i] = Array.ConvertAll(line.Split(' '), Convert.ToInt32);
            }
            sr.Close();

            long result = ArrayManipulation(n, queries);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static void Main2()
        {
            var n = 5;
            var m = 3;
            var s = "1 2 100\n" +
                    "2 5 100\n" +
                    "3 4 100\n";

            var queries = new int[m][];

            var strQueries = s.Split('\n').Where(x => !string.IsNullOrEmpty(x));
            var i = 0;
            foreach (var strQuery in strQueries)
            {
                queries[i] = Array.ConvertAll(strQuery.Split(' '), Convert.ToInt32);
                i++;
            }

            var result = ArrayManipulation(n, queries);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
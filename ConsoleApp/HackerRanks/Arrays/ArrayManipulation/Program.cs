using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp.HackerRanks.Arrays.ArrayManipulation
{
    public class Program
    {
        /// <summary>
        /// Такой метод не работает для большого количества массивов поиска,
        /// невозможно дождаться конца поиска
        /// </summary>
        private static long ArrayManipulation_SimpleSearch(int n, int[][] queries)
        {
            var arr = new long[n + 1];
            var max = long.MinValue;

            foreach (var query in queries)
            {
                var a = query[0];
                var b = query[1];
                var k = query[2];

                for (var i = a; i <= b; i++)
                {
                    if (arr[i] > int.MaxValue - k)
                    {
                    }

                    arr[i] += k;
                    if (arr[i] > max) max = arr[i];
                }
            }

            return max;
        }

        private static long ArrayManipulation(int n, int[][] queries)
        {
            var arr = new int[n + 1];

            var starts = new Dictionary<int, List<int>>();
            var ends = new Dictionary<int, List<int>>();

            for (int i = 0; i < queries.Length; i++)
            {
                var query = queries[i];
                var a = query[0];
                var b = query[1];
                var k = query[2];

                arr[a] += -1;
                arr[b] += -2;

                if (starts.TryGetValue(a, out var startValues))
                {
                    startValues.Add(k);
                }
                else
                {
                    starts.Add(a, new List<int> {k});
                }

                if (ends.TryGetValue(b, out var endValues))
                {
                    endValues.Add(k);
                }
                else
                {
                    ends.Add(b, new List<int> {k});
                }
            }

            var max = long.MinValue;
            var currentSum = 0L;

            for (int j = 1; j < n + 1; j++)
            {
                if (arr[j] < 0)
                {
                    if (ends.TryGetValue(j, out var endValues))
                    {
                        if (currentSum > max) max = currentSum;

                        foreach (var endValue in endValues)
                        {
                            currentSum -= endValue;
                        }
                    }

                    if (starts.TryGetValue(j, out var startValues))
                    {
                        foreach (var startValue in startValues)
                        {
                            currentSum += startValue;
                        }
                    }
                }
            }

            return max;
        }

        public static void Main()
        {
            var sr = new StreamReader(@".\..\..\HackerRanks\ArrayManipulation\input04.txt");
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
    }
}
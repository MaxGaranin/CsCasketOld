using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;

namespace ConsoleApp.HackerRanks.Dictionaries.CountTriplets
{
    public class Program
    {
        public static long CountTriplets(List<long> arr, long r)
        {
            if (r == 1)
            {
                var dict = new Dictionary<long, long>();

                for (var i = 0; i < arr.Count; i++)
                {
                    if (dict.TryGetValue(arr[i], out var val))
                    {
                        dict[arr[i]] = val + 1;
                    }
                    else
                    {
                        dict.Add(arr[i], 1);
                    }
                }

                var n = 0L;
                foreach (var kv in dict)
                {
                    if (kv.Value < 3) continue;

                    var count = CalculateBinom(kv.Value, 3);
                    n += count;
                }

                return n;
            }
            else
            {
                // Очень медленный способ
//                var triplets = new HashSet<List<long>>();
//
//                for (int i = 0; i < arr.Count - 2; i++)
//                {
//                    for (int j = i + 1; j < arr.Count - 1; j++)
//                    {
//                        if (arr[j] <= arr[i] || arr[j] % arr[i] > 0) continue;
//
//                        for (int k = j + 1; k < arr.Count; k++)
//                        {
//                            if (arr[k] <= arr[j] || arr[k] % arr[j] > 0) continue;
//
//                            triplets.Add(new List<long> {arr[i], arr[j], arr[k]});
//                        }
//                    }
//                }
//
//                var n = triplets.Count;
//                return n;

                var dict = arr.GroupBy(g => g)
                    .ToDictionary(x => x.Key, y => (long) y.Count());

                var n = 0L;
                for (int i = 0; i < arr.Count; i++)
                {
                    var t = arr[i] * r;
                    var t2 = t * r;
                    if (dict.ContainsKey(t) &&
                        dict.ContainsKey(t2))
                    {
                        var k = dict[t];
                        var k2 = dict[t2];
                        n += k * k2;
                    }
                }

                return n;
            }
        }

        private static long CalculateBinom(long n, long m)
        {
            if (n == m) return 1L;

            var above = 1L;
            for (var i = n - m + 1; i <= n; i++)
            {
                above *= i;
            }

            var below = Factorial(m);

            var result = above / below;
            return result;
        }

        private static long Factorial(long m)
        {
            if (m == 0 || m == 1) return 1;
            return m * Factorial(m - 1);
        }

        public static void Main()
        {
//            var arr = new List<long> {1, 3, 9, 9, 27, 81};
//            var r = 3L;
//            var n = CountTriplets(arr, r);
//            Console.WriteLine($"Result: {n}");

            var textReader = new StreamReader(@".\..\..\HackerRanks\Dictionaries\CountTriplets\input06.txt");
            var line = textReader.ReadLine();
            var tokens = line.Split(' ').Select(long.Parse).ToList();
            var r = tokens[1];
            line = textReader.ReadLine();
            var arr = line.Split(' ').Select(long.Parse).ToList();
            textReader.Close();

            var n = CountTriplets(arr, r);
            Console.WriteLine($"Result: {n}");

            Console.ReadKey();
        }
    }
}
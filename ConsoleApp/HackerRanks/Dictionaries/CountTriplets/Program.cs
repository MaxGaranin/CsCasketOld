using System;
using System.Collections.Generic;
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
                #region Очень медленный способ

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

                #endregion

                #region Не прошло 2 теста

//                var dict = arr.GroupBy(g => g)
//                    .ToDictionary(x => x.Key, y => (long) y.Count());
//
//                var n = 0L;
//                for (int i = 0; i < arr.Count; i++)
//                {
//                    var t = arr[i] * r;
//                    var t2 = t * r;
//                    if (dict.ContainsKey(t) &&
//                        dict.ContainsKey(t2))
//                    {
//                        var k = dict[t];
//                        var k2 = dict[t2];
//                        n += k * k2;
//                    }
//                }
//
//                return n;

                #endregion

                #region 3 словаря, счет после

//                var dict1 = new Dictionary<long, List<long>>();
//                var dict2 = new Dictionary<long, List<long>>();
//                var dict3 = new Dictionary<long, List<long>>();
//
//                for (int i = 0; i < arr.Count; i++)
//                {
//                    var val = arr[i];
//
//                    if (!dict1.ContainsKey(val)) dict1[val] = new List<long>();
//                    dict1[val].Add(i);
//
//                    if (val % r == 0)
//                    {
//                        if (!dict2.ContainsKey(val)) dict2[val] = new List<long>();
//                        dict2[val].Add(i);
//
//                        var val2 = val / r;
//                        if (val2 % r == 0)
//                        {
//                            if (!dict3.ContainsKey(val)) dict3[val] = new List<long>();
//                            dict3[val].Add(i);
//                        }
//                    }
//                }
//
//                var n = 0;
//                foreach (var kv3 in dict3)
//                {
//                    foreach (var index3 in kv3.Value)
//                    {
//                        var v2 = kv3.Key / r;
//                        if (!dict2.TryGetValue(v2, out var indices2)) continue;
//
//                        indices2 = indices2.Where(x => x < index3).ToList();
//
//                        foreach (var index2 in indices2)
//                        {
//                            var v1 = v2 / r;
//                            if (!dict1.TryGetValue(v1, out var indices1)) continue;
//
//                            indices1 = indices1.Where(x => x < index2).ToList();
//                            n += indices1.Count;
//                        }
//                    }
//                }
//
//                return n;

                #endregion

                #region 3 словаря, счет во время прохода

                var dictCand1 = new Dictionary<long, long>();
                var dict1 = new Dictionary<long, long>();
                var dict2 = new Dictionary<long, long>();
                var dict3 = new Dictionary<long, long>();
                var n = 0L;

                for (int i = 0; i < arr.Count; i++)
                {
                    var val = arr[i];

                    if (!dictCand1.ContainsKey(val)) dictCand1.Add(val, 0);
                    dictCand1[val]++;

                    if (val % r == 0)
                    {
                        if (!dict2.ContainsKey(val)) dict2.Add(val, 0);
                        dict2[val]++;

                        var val2 = val / r;

                        if (dictCand1.ContainsKey(val2))
                        {
                            if (!dict1.ContainsKey(val2)) dict1.Add(val2, 0);
                            dict1[val2] += dictCand1[val2];
                            dictCand1.Remove(val2);
                        }

                        if (val2 % r == 0)
                        {
                            if (!dict3.ContainsKey(val)) dict3.Add(val, 0);
                            dict3[val]++;

                            var val1 = val2 / r;
                            if (dict2.ContainsKey(val2) && dict1.ContainsKey(val1))
                            {
                                var n2 = dict2[val2];
                                var n1 = dict1[val1];
                                n += n1 * n2;
                            }
                        }
                    }
                }

                return n;

                #endregion
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
            {
//                var arr = new List<long> {1, 3, 9, 9, 27, 81};
//                var r = 3L;
//                var n = CountTriplets(arr, r);
//                Console.WriteLine($"Result: {n}");
            }

            {
                var textReader = new StreamReader(@".\..\..\HackerRanks\Dictionaries\CountTriplets\input10.txt");
                var line = textReader.ReadLine();
                var tokens = line.Split(' ').Select(long.Parse).ToList();
                var r = tokens[1];
                line = textReader.ReadLine();
                var arr = line.Split(' ').Select(long.Parse).ToList();
                textReader.Close();

                var n = CountTriplets(arr, r);
                Console.WriteLine($"Result: {n}");
            }

            Console.ReadKey();
        }
    }
}
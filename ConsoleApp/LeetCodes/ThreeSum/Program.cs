using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp.LeetCodes.ThreeSum
{
    public class Program
    {
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var results = new List<IList<int>>();

            Array.Sort(nums);

            int i1 = 0;
            int i2 = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i1]) continue;

                i1 = i - 1;
                i2 = i;

                for (int j = i; j < nums.Length; j++)
                {
                    if (nums[j] == nums[i2]) continue;
                    i2 = j - 1;

                    var k = nums[i1] + nums[i2];
                    if (k > 0 && nums[j] > 0) break;

                    var i3 = Array.BinarySearch(nums, j, nums.Length - j, -k);
                    if (i3 > 0)
                    {
                        var result = new List<int> {nums[i1], nums[i2], nums[i3]};
                        results.Add(result);
                    }

                    i2 = j;
                }

                i1 = i;
            }

            return results;
        }

        public static void Main()
        {
            {
                var nums = new int[] {-4, -2, 1, -5, -4, -4, 4, -2, 0, 4, 0, -2, 3, 1, -5, 0, 9};
                var result = ThreeSum(nums);
                PrintResult(result);
            }

            Console.ReadKey();
            return;

            {
                var sr = new StreamReader(@".\..\..\LeetCodes\ThreeSum\input01.txt");
                var s = sr.ReadLine();
                var nums = s.Split(',').Select(int.Parse).ToArray();
                sr.Close();

                var result = ThreeSum(nums);
                PrintResult(result);
            }

            Console.ReadKey();
        }

        private static void PrintResult(IList<IList<int>> result)
        {
            foreach (var item in result)
            {
                var str = string.Join(", ", item);
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }
    }
}
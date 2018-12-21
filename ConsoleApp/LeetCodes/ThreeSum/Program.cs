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

            for (var i = 0; i < nums.Length - 2; i++)
            {
                for (var j = i + 1; j < nums.Length - 1; j++)
                {
                    for (var k = j + 1; k < nums.Length; k++)
                    {
                        var sum = nums[i] + nums[j] + nums[k];
                        if (sum == 0)
                        {
                            var test = new List<int>
                            {
                                nums[i], nums[j], nums[k]
                            };
                            test.Sort();

                            var isDuplicate = false;
                            foreach (var result in results)
                            {
                                if (result.SequenceEqual(test))
                                {
                                    isDuplicate = true;
                                    break;
                                }
                            }

                            if (isDuplicate) break;

                            var newResult = new List<int>
                            {
                                nums[i], nums[j], nums[k]
                            };
                            newResult.Sort();

                            results.Add(newResult);
                        }
                    }
                }
            }

            return results;
        }

        public static void Main()
        {
            var sr = new StreamReader(@".\..\..\LeetCodes\ThreeSum\input01.txt");
            var s = sr.ReadLine();
            var nums = s.Split(',').Select(int.Parse).ToArray();
            sr.Close();

            var result = ThreeSum(nums);
            foreach (var item in result)
            {
                var str = string.Join(",", item);
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }
    }
}
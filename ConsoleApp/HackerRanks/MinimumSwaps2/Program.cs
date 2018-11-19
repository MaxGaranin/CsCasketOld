using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConsoleApp.HackerRanks.MinimumSwaps2
{
    public class Program
    {
        #region Пузырьковые сортировки (сортировки обменом)

        /// <summary>
        /// Обычная пузырьковая сортировка
        /// <remarks>Поиск максимума в прямом направлении</remarks>
        /// </summary>
        public static int BubbleSort(int[] a)
        {
            var count = 0;

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length - 1 - i; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        Swap(a, j, j + 1);
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Шейкерная сортировка
        /// <remarks>Поиск максимума в прямом направлении и минимума в обратном</remarks>
        /// </summary>
        public static int ShakerSort(int[] a)
        {
            var count = 0;

            var i1 = 0;
            var i2 = a.Length - 1;

            while (i1 < i2)
            {
                for (int j = i1; j < i2; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        Swap(a, j, j + 1);
                        count++;
                    }
                }

                i2--;

                for (int j = i2; j > i1; j--)
                {
                    if (a[j] < a[j - 1])
                    {
                        Swap(a, j, j - 1);
                        count++;
                    }
                }

                i1++;
            }

            return count;
        }

        /// <summary>
        /// Четно-нечетная сортировка
        /// <remarks>
        /// Идем только в прямом напрвалении.
        /// Сначала проходим по четным индексам, потом по нечетным
        /// </remarks>
        /// </summary>
        public static int OddEvenSort(int[] a)
        {
            var count = 0;

            var i1 = 0;
            var i2 = 1;

            while (true)
            {
                var savedCount = count;

                for (int j = i1; j < a.Length - 1; j = j + 2)
                {
                    if (a[j] > a[j + 1])
                    {
                        Swap(a, j, j + 1);
                        count++;
                    }
                }

                for (int j = i2; j < a.Length - 1; j = j + 2)
                {
                    if (a[j] > a[j + 1])
                    {
                        Swap(a, j, j + 1);
                        count++;
                    }
                }

                if (count == savedCount) break;
            }

            return count;
        }

        /// <summary>
        /// Сортировка расческой
        /// <remarks> Выбирается шаг для прохождения по массиву</remarks>
        /// </summary>
        public static int BrushSort(int[] a)
        {
            const double factor = 1.247;

            var count = 0;

            var i1 = 0;
            var i2 = 1;
            var k = a.Length;

            while (true)
            {
                k = (int) Math.Floor(k / factor);
                if (k == 1) break;

                for (int j = 0; j < a.Length - 1; j = j + k)
                {
                    if (j + k >= a.Length) break;

                    if (a[j] > a[j + k])
                    {
                        Swap(a, j, j + k);
                        count++;
                    }
                }
            }

            var res = BubbleSort(a);

            return count + res;
        }

        #endregion

        #region Сортировки выбором

        /// <summary>
        /// Обычная сортировка выбором
        /// </summary>
        public static int SelectionSort(int[] a)
        {
            var count = 0;

            for (int i = 0; i < a.Length; i++)
            {
                var min = i;
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }

                if (i != min)
                {
                    Swap(a, i, min);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Циклическая сортировка
        /// </summary>
        public static int CycleSort(int[] a)
        {
            var count = 0;

            for (int start = 0; start < a.Length; start++)
            {
                var value = a[start];

                var pos = start;
                var j = start + 1;
                while (j < a.Length)
                {
                    if (a[j] < value) pos++;
                    j++;
                }

                if (pos == start) continue;

                while (value == a[pos]) pos++;

                var tmp = a[pos];
                a[pos] = value;
                value = tmp;
                count++;

                while (pos != start)
                {
                    pos = start;
                    j = start + 1;
                    while (j < a.Length)
                    {
                        if (a[j] < value) pos++;
                        j++;
                    }

                    if (pos == start)
                    {
                        a[pos] = value;
                        continue;
                    }

                    while (value == a[pos]) pos++;

                    tmp = a[pos];
                    a[pos] = value;
                    value = tmp;
                    count++;
                }
            }

            return count;
        }

        #endregion

        #region Сортировки вставками

        /// <summary>
        /// Сортировка вставками
        /// </summary>
        public static int InsertionsSort(int[] a)
        {
            var count = 0;

            for (var i = 1; i < a.Length; i++)
            {
                var key = a[i];

                var j = i - 1;

                while (j >= 0 && a[j] > key)
                {
                    a[j + 1] = a[j];
                    j--;
                }

                a[j + 1] = key;
                count++;
            }

            return count;
        }

        #endregion

        #region Быстрая сортировка

        private static int QuickSort(int[] a)
        {
            return QuickSortInPlace(a, 0, a.Length - 1);
        }

        private static int QuickSortInPlace(int[] a, int start, int end)
        {
            var count = 0;
            if (start >= end) return count;

            var divider = a[start];

            var lo = start;
            var hi = end;
            while (true)
            {
                while (a[hi] >= divider)
                {
                    hi--;
                    if (hi <= lo) break;
                }

                if (hi <= lo)
                {
                    a[lo] = divider;
                    break;
                }

                a[lo] = a[hi];
                count++;

                lo++;
                while (a[lo] <= divider)
                {
                    lo++;
                    if (lo >= hi) break;
                }

                if (lo >= hi)
                {
                    a[hi] = divider;
                    break;
                }

                a[hi] = a[lo];
                hi--;
                count++;
            }

            var c1 = QuickSortInPlace(a, start, lo - 1);
            count += c1;

            var c2 = QuickSortInPlace(a, lo + 1, end);
            count += c2;

            return count;
        }

        private static int QuickSortWithStacks_MySelf(int[] a, int start, int end)
        {
            var count = 0;
            if (start >= end) return 0;

            var divider = a[start];

            var loValues = new int[end - start + 1];
            var hiValues = new int[end - start + 1];

            var lo = 0;
            var hi = 0;

            for (int i = start + 1; i <= end; i++)
            {
                if (a[i] >= divider)
                {
                    hiValues[hi] = a[i];
                    hi++;
                    count++;
                }
                else
                {
                    loValues[lo] = a[i];
                    lo++;
                    count++;
                }
            }

            a[lo] = divider;

            if (lo > 0)
            {
                var c1 = QuickSortWithStacks_MySelf(loValues, 0, lo - 1);
                count += c1;
            }

            if (hi > 0)
            {
                var c2 = QuickSortWithStacks_MySelf(hiValues, 0, hi - 1);
                count += c2;
            }

            for (int i = 0; i < lo; i++)
            {
                a[i] = loValues[i];
            }

            for (int i = 0; i < hi; i++)
            {
                a[lo + 1 + i] = hiValues[i];
            }

            return count;
        }

        private static int QuickSortWithStacks_FromBook(int[] a, int start, int end)
        {
            var count = 0;
            if (start >= end) return 0;

            var divider = a[start];

            var loValues = new Stack<int>();
            var hiValues = new Stack<int>();

            for (int i = start + 1; i <= end; i++)
            {
                if (a[i] >= divider)
                {
                    hiValues.Push(a[i]);
                }
                else
                {
                    loValues.Push(a[i]);
                }
            }

            var j = start;
            foreach (var loValue in loValues)
            {
                a[j] = loValue;
                j++;
                count++;
            }

            var dividerIndex = j;
            a[j] = divider;
            j++;

            foreach (var hiValue in hiValues)
            {
                a[j] = hiValue;
                j++;
                count++;
            }

            var c1 = QuickSortWithStacks_FromBook(a, start, dividerIndex - 1);
            var c2 = QuickSortWithStacks_FromBook(a, dividerIndex + 1, end);

            count += c1;
            count += c2;

            return count;
        }

        #endregion

        #region Сортировка слиянием

        private static int MergeSort(int[] a)
        {
            var scratch = new int[a.Length];
            return MergeSortRecursive(a, scratch, 0, a.Length - 1);
        }

        private static int MergeSortRecursive(int[] a, int[] scratch, int start, int end)
        {
            var count = 0;
            if (end <= start) return 0;

            var midPoint = (start + end) / 2;

            var c1 = MergeSortRecursive(a, scratch, start, midPoint);
            var c2 = MergeSortRecursive(a, scratch, midPoint + 1, end);
            count += c1;
            count += c2;

            var leftIndex = start;
            var rightIndex = midPoint + 1;
            var scratchIndex = start;
            while (leftIndex <= midPoint && rightIndex <= end)
            {
                if (a[leftIndex] <= a[rightIndex])
                {
                    scratch[scratchIndex] = a[leftIndex];
                    leftIndex++;
                }
                else
                {
                    scratch[scratchIndex] = a[rightIndex];
                    rightIndex++;
                    count++;
                }

                scratchIndex++;
            }

            for (int i = leftIndex; i <= midPoint; i++)
            {
                scratch[scratchIndex] = a[i];
                scratchIndex++;
            }

            for (int i = rightIndex; i <= end; i++)
            {
                scratch[scratchIndex] = a[i];
                scratchIndex++;
            }

            for (int i = start; i <= end; i++)
            {
                a[i] = scratch[i];
            }

            return count;
        }

        #endregion

        #region Пирамидальная сортировка

        private static int HeapSort(int[] a)
        {
            var count = 0;

            MakeHeap(a);

            for (int i = a.Length - 1; i >= 1; i--)
            {
                var topValue = RemoveTopItem(a, i + 1);
                a[i] = topValue;
            }

            return count;
        }

        private static void MakeHeap(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                var index = i;
                while (index != 0)
                {
                    var parent = (index - 1) / 2;

                    if (a[index] <= a[parent]) break;

                    Swap(a, index, parent);

                    index = parent;
                }
            }
        }

        private static int RemoveTopItem(int[] a, int count)
        {
            var result = a[0];

            a[0] = a[count - 1];

            var index = 0;
            while (true)
            {
                var child1 = 2 * index + 1;
                var child2 = 2 * index + 2;

                if (child1 >= count) child1 = index;
                if (child2 >= count) child2 = index;

                if (a[index] >= a[child1] &&
                    a[index] >= a[child2]) break;

                int swapChild;
                if (a[child1] >= a[child2])
                {
                    swapChild = child1;
                }
                else
                {
                    swapChild = child2;
                }

                Swap(a, index, swapChild);
                index = swapChild;
            }

            return result;
        }

        #endregion

        #region Сортировка подсчетом

        private static int CountingSort(int[] a)
        {
            var k = 0;
            if (a.Length == 0) return 0;

            var maxValue = a.Max();
            var counts = new int[maxValue + 1];

            for (int i = 0; i < a.Length; i++)
            {
                var value = a[i];
                counts[value]++;
            }

            var index = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 1; j <= counts[i]; j++)
                {
                    a[index] = i;
                    index++;
                    k++;
                }
            }

            return k;
        }

        #endregion

        private static void Swap(int[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        /// <summary>
        /// Реализация сортировки с минимальными перестановками в предположении,
        /// что в массиве только целые числа без пропусков и дубликатов
        /// </summary>
        private static int MySort(int[] a)
        {
            var count = 0;

            var i = 0;
            while (i < a.Length)
            {
                if (a[i] == i + 1)
                {
                    i++;
                }
                else
                {
                    Swap(a, i, a[i] - 1);
                    count++;
                }
            }

            return count;
        }

        public static void Main()
        {
            var s = "";
            s = "2 3 4 1 5";
            s = "3 7 6 9 1 8 10 4 2 5";

            var arr = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
            var res = MySort(arr);
            Console.WriteLine(string.Join(" ", arr));
            Console.WriteLine(res);
            Console.WriteLine();

//            Console.ReadKey();
//            return;
            
//            var sr = new StreamReader(@".\..\..\HackerRanks\MinimumSwaps2\input09.txt");
//            sr.ReadLine();
//            s = sr.ReadLine();
//
//            var sw = new Stopwatch();
//            Console.WriteLine("Merge sort");
//            arr = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
//            sw.Start();
//            res = MergeSort(arr);
//            sw.Stop();
//            Console.WriteLine("Elapsed: {0}", sw.Elapsed);
////            Console.WriteLine(string.Join(" ", arr));
//            Console.WriteLine(res);
//            Console.WriteLine();
//
//            Console.WriteLine("Counting sort");
//            arr = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
//            sw.Start();
//            res = CountingSort(arr);
//            sw.Stop();
//            Console.WriteLine("Elapsed: {0}", sw.Elapsed);
////            Console.WriteLine(string.Join(" ", arr));
//            Console.WriteLine(res);
//            Console.WriteLine();
//
//            Console.WriteLine("Quick sort");
//            arr = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
//            sw.Start();
//            res = QuickSort(arr);
//            sw.Stop();
//            Console.WriteLine("Elapsed: {0}", sw.Elapsed);
////            Console.WriteLine(string.Join(" ", arr));
//            Console.WriteLine(res);
//            Console.WriteLine();
//
//            Console.WriteLine("Cycle sort");
//            arr = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
//            sw.Start();
//            res = CycleSort(arr);
//            sw.Stop();
//            Console.WriteLine("Elapsed: {0}", sw.Elapsed);
////            Console.WriteLine(string.Join(" ", arr));
//            Console.WriteLine(res);
//            Console.WriteLine();
//            
//            Console.ReadKey();
//            return;

            var rnd = new Random();
            var k = 100;
            var arr2 = new int[k];
            for (int i = 0; i < k; i++)
            {
                arr2[i] = rnd.Next(1000);
            }

            res = MergeSort(arr2);
            Console.WriteLine(string.Join(" ", arr2));
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
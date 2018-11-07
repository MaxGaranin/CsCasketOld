using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp.HackerRanks.MinimumSwaps2
{
    public static class MyExtensions
    {
        public static string MyDoubleString(this string s)
        {
            if (s == null) return null;
            return s + s;
        }
    }

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
            return QuickSortRecursive(a, 0, a.Length - 1);
        }

        private static int QuickSortRecursive(int[] a, int left, int right)
        {
            var count = 0;
            if (a.Length == 1) return count;

            var l = left;
            var r = right;
            var baseValue = a[(l + r) / 2];

            while (l < r)
            {
                while (a[l] < baseValue) { l++; }

                while (a[r] > baseValue) { r--; }

                if (l < r)
                {
                    Swap(a, l, r);
                    count++;
                }
            }

            if (left < r - 1)
            {
                var c1 = QuickSortRecursive(a, left, r - 1);
                count += c1;
            }

            if (right > r + 1)
            {
                var c2 = QuickSortRecursive(a, r + 1, right);
                count += c2;
            }

            return count;
        }

        #endregion

        private static void Swap(int[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        public static void Main()
        {
            string str = "a";
            str = str.MyDoubleString();

            str = null;
            str = str.MyDoubleString();
            
            var s = "2 3 4 1 5";
            int[] arr = Array.ConvertAll(s.Split(' '), Convert.ToInt32);
            var res = InsertionsSort(arr);
            Console.WriteLine(string.Join(" ", arr));
            Console.WriteLine(res);
            Console.WriteLine();

            Console.ReadKey();
            return;

            var rnd = new Random();
            var k = 100;
            var arr2 = new int[k];
            for (int i = 0; i < k; i++)
            {
                arr2[i] = rnd.Next(1000);
            }

            res = QuickSort(arr2);
            Console.WriteLine(string.Join(" ", arr2));
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
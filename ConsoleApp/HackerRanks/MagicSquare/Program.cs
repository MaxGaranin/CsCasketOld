using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.HackerRanks.MagicSquare
{
    public class Program
    {
        public static void Main()
        {
            var input = new int[][]
            {
                new[] {4, 8, 2},
                new[] {4, 5, 7},
                new[] {6, 1, 6}
            };

            var result = FormingMagicSquare(input);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static int FormingMagicSquare(int[][] ss)
        {
            const int count = 9;

            var sList = new List<int>();
            for (int i = 0; i < ss.Length; i++)
            {
                for (int j = 0; j < ss[i].Length; j++)
                {
                    sList.Add(ss[i][j]);
                }
            }

            var s = sList.ToArray();

            var magicSquares = CreateMagicSquares();
            var minDiff = int.MaxValue;
            foreach (var magicSquare in magicSquares)
            {
                var diff = 0;
                for (int i = 0; i < count; i++)
                {
                    diff += Math.Abs(magicSquare[i] - s[i]);
                }

                if (diff < minDiff) minDiff = diff;
            }

            return minDiff;
        }

        private static List<int[]> CreateMagicSquares()
        {
            const int count = 9;

            // Получение всех перестановок
            var sequenceList = new List<int[]>();
            var sequence = new int[count];
            InitSequence(sequence);
            do
            {
                sequenceList.Add(sequence.ToArray()); // добавляем копию
            } while (Narayana.NextPermutation(sequence, Less));

            // Получение магических квадратов
            var magicSquares = new List<int[]>();
            for (int i = 0; i < sequenceList.Count; i++)
            {
                var a = sequenceList[i];
                var sum = a[0] + a[1] + a[2];
                if (a[3] + a[4] + a[5] == sum &&
                    a[6] + a[7] + a[8] == sum &&
                    a[0] + a[3] + a[6] == sum &&
                    a[1] + a[4] + a[7] == sum &&
                    a[2] + a[5] + a[8] == sum &&
                    a[0] + a[4] + a[8] == sum &&
                    a[2] + a[4] + a[6] == sum)
                {
                    magicSquares.Add(a);
                }
            }

            return magicSquares;
        }

        private static void TestNarayana()
        {
            var count = int.Parse(Console.ReadLine());
            var sequence = new int[count];
            InitSequence(sequence); // Формирование исходной последовательности

            Console.WriteLine("Неубывающая последовательность и её перестановки:");
            // x < y — критерий сравнения для неубывающей последовательности
            do
            {
                OutputSequence(sequence);
            } while (Narayana.NextPermutation(sequence, Less));

            Console.WriteLine("Невозрастающая последовательность и её перестановки:");
            // x > y — критерий сравнения для невозрастающей последовательности
            do
            {
                OutputSequence(sequence);
            } while (Narayana.NextPermutation(sequence, Greater));

            Console.ReadKey();
        }

        /// <summary>
        /// Возвращает true, если value_0 меньше value_1, иначе — false
        /// </summary>
        private static bool Less<T>(T value_0, T value_1) where T : IComparable
        {
            return value_0.CompareTo(value_1) < 0;
        }

        /// <summary>
        /// Возвращает true, если value_0 больше value_1, иначе — false
        /// </summary>
        private static bool Greater<T>(T value_0, T value_1) where T : IComparable
        {
            return value_0.CompareTo(value_1) > 0;
        }

        /// <summary>
        /// Инициализация последовательности
        /// </summary>
        private static void InitSequence(int[] sequence)
        {
            // Заполнение последовательности значениями 1, 2, 3…
            for (var i = sequence.Length; i > 0; --i)
            {
                sequence[i - 1] = i;
            }
        }

        /// <summary>
        /// Вывод содержимого последовательности
        /// </summary>
        private static void OutputSequence<T>(T[] sequence)
        {
            Console.Write('[');

            if (!(sequence == null) && (sequence.Length > 0))
            {
                Console.Write(sequence[0]);
                for (var i = 1; i < sequence.Length; ++i)
                {
                    Console.Write(", ");
                    Console.Write(sequence[i]);
                }
            }

            Console.WriteLine(']');
        }
    }
}
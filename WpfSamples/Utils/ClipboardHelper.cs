using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfSamples40.Utils
{
    public class ClipboardHelper
    {
        public static string[,] GetArrayFromClipboard()
        {
            var lineTokens = ReadList2DFromClipboard();

            return CreateRectangularArray(lineTokens);
        }

        public static string[][] GetJuggedArrayFromClipboard()
        {
            var lineTokens = ReadList2DFromClipboard();
            return lineTokens != null ? lineTokens.Select(l => l.ToArray()).ToArray() : null;
        }


        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private static IList<IList<string>> ReadList2DFromClipboard()
        {
            var clipboardString = Clipboard.GetText();
            var lines = clipboardString.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
                return null;

            var lineTokens = new List<IList<string>>();
            foreach (var line in lines)
            {
                var tokens = line.Split(new char[] { '\t' }, StringSplitOptions.None)
                                 .ToList();
                lineTokens.Add(tokens);
            }

            return lineTokens;
        }


        private static T[,] CreateRectangularArray<T>(IList<IList<T>> list2D)
        {
            if ((list2D.Count == 0) || (list2D[0].Count == 0))
            {
                throw new ArgumentException("CreateRectangularArray. Пустой список на входе");
            }

            int minorLength = list2D[0].Count;

            T[,] ret = new T[list2D.Count, minorLength];
            for (int i = 0; i < list2D.Count; i++)
            {
                var array = list2D[i];
                if (array.Count != minorLength)
                {
                    throw new ArgumentException("CreateRectangularArray. Двумерный массив должен иметь одинаковую длину элементов");
                }

                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }

            return ret;
        }

        private static T[,] CreateRectangularArray<T>(T[][] juggedArray)
        {
            if ((juggedArray.GetLength(0) == 0) || (juggedArray.GetLength(1) == 0))
            {
                throw new ArgumentException("CreateRectangularArray. Пустой массив");
            }

            int minorLength = juggedArray[0].Length;

            T[,] ret = new T[juggedArray.Length, minorLength];
            for (int i = 0; i < juggedArray.Length; i++)
            {
                var array = juggedArray[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException("CreateRectangularArray. Двумерный массив должен иметь одинаковую длину элементов");
                }

                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }

            return ret;
        }
    }
}

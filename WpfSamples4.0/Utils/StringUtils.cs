using System;
using System.Globalization;
using System.Text;

namespace WpfSamples40.Utils
{
    public enum StringPadDirection
    {
        Left,
        Right
    }

    public static class StringUtils
    {
        public static readonly char[] DEL_WHITESPACES = {' ', '\t'};
        public static readonly string DEC_SEP = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

        public static readonly string COMMA = ",";
        public static readonly string POINT = ".";


        public static string[] GetTokens(this string sLine, char[] separator)
        {
            return sLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetTokens(this string sLine)
        {
            return GetTokens(sLine, DEL_WHITESPACES);
        }

        public static string[] GetTokens(this string sLine, bool convertDecSep)
        {
            return GetTokens(sLine, DEL_WHITESPACES, convertDecSep);
        }

        public static string[] GetTokens(this string sLine, char[] separator, bool convertDecSep)
        {
            if (convertDecSep)
            {
                sLine = DecSepToSys(sLine);
            }
            return GetTokens(sLine, separator);
        }

        public static string DecSepToSys(this string s, string sep)
        {
            return s.Replace(sep, DEC_SEP);
        }

        public static string DecSepToSys(this string s)
        {
            s = s.Replace(POINT, DEC_SEP);
            s = s.Replace(COMMA, DEC_SEP);
            return s;
        }

        public static string SysSepToDec(this string s)
        {
            return SysSepToDec(s, POINT);
        }

        public static string SysSepToDec(this string s, string sep)
        {
            return s.Replace(DEC_SEP, sep);
        }

        public static double StrToDouble(this string s)
        {
            return double.Parse(DecSepToSys(s));
        }

        public static int StrToInt(this string s)
        {
            return int.Parse(s);
        }

        public static string DoubleToStr(this double value)
        {
            return SysSepToDec(value.ToString());
        }

        public static string DoubleToStr(this double value, string format)
        {
            return SysSepToDec(value.ToString(format));
        }

        public static string DoubleToStr(this double value, StringPadDirection direction, int pad)
        {
            string s = SysSepToDec(value.ToString());
            switch (direction)
            {
                case StringPadDirection.Left:
                    return PadLeft(s, pad);
                case StringPadDirection.Right:
                    return PadRight(s, pad);
                default:
                    return s;
            }
        }

        public static string StrLineRightPad(int pad, params string[] str)
        {
            StringBuilder sbOut = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (pad > str[i].Length)
                    sbOut.Append(str[i].PadRight(pad));
                else
                    sbOut.Append(str[i] + "  ");
            }
            return sbOut.ToString();
        }

        public static string StrLineRightPad(int pad, bool changeSysToDec, params double[] array)
        {
            string[] str = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                str[i] = array[i].ToString();
                if (changeSysToDec)
                    str[i] = SysSepToDec(str[i]);
            }
            return StrLineRightPad(pad, str);
        }

        public static string StrLineRightPad(int pad, bool changeSysToDec, bool trimEnd, params double[] array)
        {
            string s = StrLineRightPad(pad, changeSysToDec, array);
            if (trimEnd) s = s.TrimEnd(null);
            return s;
        }

        public static string PadRight(this string s, int pad)
        {
            if (s.Length < pad)
                return s.PadRight(pad);
            else
                return s + " ";
        }

        public static string PadLeft(this string s, int pad)
        {
            if (s.Length < pad)
                return s.PadLeft(pad);
            else
                return " " + s;
        }
    }
}

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace eNotification
{
    public static class StringFormatExtension
    {
        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }

        public static string ToMD5(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(checkSum).Replace("-", String.Empty);
        }

        public static string AddCountryCode(this string str)
        {
            return "7" + str;
        }

        public static string RemoveWhitespace(this string str)
        {
            return new string(str.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpDevTest.Data
{
    public static class Code
    {
        /// <summary>
        /// поиск наибольшей подстроки
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string LCS(string s1, string s2)
        {
            s1 = s1.ToLower();
            s2 = s2.ToLower();
            var a = new int[s1.Length + 1, s2.Length + 1];
            int u = 0, v = 0;

            for (var i = 0; i < s1.Length; i++)
                for (var j = 0; j < s2.Length; j++)
                    if (s1[i] == s2[j])
                    {
                        a[i + 1, j + 1] = a[i, j] + 1;
                        if (a[i + 1, j + 1] > a[u, v])
                        {
                            u = i + 1;
                            v = j + 1;
                        }
                    }

            return s1.Substring(u - a[u, v], a[u, v]);
        }

        public static string ToLongString(this Guid value)
        {
            byte[] gb = Guid.NewGuid().ToByteArray(); 
            long l = BitConverter.ToInt64(gb, 0);
            return l.ToString();
        }

        public static Guid ToGuid(this long value)
        {
            byte[] guidData = new byte[16];
            Array.Copy(BitConverter.GetBytes(value), guidData, 8);
            return new Guid(guidData);
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiliStart.Danmaku
{
    public class Utils
    {
        private static Random ra = new Random();

        public static string GetRandomString(int _Length)
        {
            string _strList = "qwertyuioplkjhgfdsazxcvbnm1234567890";
            string _buffer = "";
            for (int i = 1; i <= _Length; i++)
            {
                _buffer += _strList[ra.Next(0, 35)];
            }
            return _buffer;
        }

        public static int GetRandomInt(int min, int max) {
            return ra.Next(min, max);
        }
    }
}

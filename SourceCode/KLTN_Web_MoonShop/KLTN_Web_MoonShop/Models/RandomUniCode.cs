using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_Web_MoonShop.Models
{
    public class RandomUniCode
    {
        public static string RandomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }
    }
}
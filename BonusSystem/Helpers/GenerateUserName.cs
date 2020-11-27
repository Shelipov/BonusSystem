using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Helpers
{
    public static class GenerateUserName
    {
        public static string GenerateRandomUsername()
        {
            string rv = "";

            char[] lowers = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] uppers = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            int l = lowers.Length;
            int u = uppers.Length;
            int n = numbers.Length;

            Random random = new Random();

            rv += lowers[random.Next(0, l)].ToString();
            rv += lowers[random.Next(0, l)].ToString();
            rv += lowers[random.Next(0, l)].ToString();

            rv += uppers[random.Next(0, u)].ToString();
            rv += uppers[random.Next(0, u)].ToString();
            rv += uppers[random.Next(0, u)].ToString();

            rv += numbers[random.Next(0, n)].ToString();
            rv += numbers[random.Next(0, n)].ToString();
            rv += numbers[random.Next(0, n)].ToString();

            return rv;
        }
    }
}

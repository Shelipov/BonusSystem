using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Helpers
{
    public static class GeneratePhoneNumber
    {
        public static string GenerateRandomPhoneNumber()
        {

            var Operator = new List<string> { "97", "50", "66", "63", "93", "95", "67" };
            var random = new Random();

            int index = random.Next(Operator.Count);

            return $"+380{Operator[index]}-{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}-{random.Next(0, 9)}{random.Next(0, 9)}-{random.Next(0, 9)}{random.Next(0, 9)}";
        }
    }
}

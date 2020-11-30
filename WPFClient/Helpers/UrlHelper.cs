using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Helpers
{
    public static class UrlHelper
    {
        public const string Domain = "http://localhost:5000/";
        public const string GetCards = "bonus-system/get-cards";
        public const string GetCardByNumberPhone = "bonus-system/get-card-by-phone-number";
        public const string GetCardBalanseByCardNumber = "bonus-system/get-card-by-number";
        public const string GetCardBalanseByUser = "bonus-system/get-card-by-user";
        public const string MoneyFromBonusCard = "bonus-system/money-from-card";
        public const string MoneyToBonusCard = "bonus-system/money-to-card";
        public const string CreateBonusCard = "bonus-system/create-card";
    }
}

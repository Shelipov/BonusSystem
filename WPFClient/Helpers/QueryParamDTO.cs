using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Helpers
{
    public class QueryParamDTO
    {
        public string UserPhoneNumber { get; set; }
        public int? UserID { get; set; }
        public int? ClientID { get; set; }
        public int? BonusCardID { get; set; }
        public string BonusCardNumber { get; set; }
        public DateTime? BonusCardTimeEnd { get; set; }
        public decimal? BonusCardBalanse { get; set; }
        public string UserEmail { get; set; }
        public decimal? MoneyFromBonusCard { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BonusSystem.EnterpriseDB.Models
{
    public class BonusCard
    {
        public int BonusCardID { get; set; }
        public string BonusCardNumber { get; set; }
        public DateTime BonusCardTimeEnd { get; set; }
        public decimal BonusCardBalanse { get; set; }
        public DateTime LastChangedOn { get; set; }
    }
}

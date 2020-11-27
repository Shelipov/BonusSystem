using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BonusSystem.EnterpriseDB.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public User User { get; set; }
        public BonusCard BonusCard { get; set; }
        public DateTime LastChangedOn { get; set; }
    }
}

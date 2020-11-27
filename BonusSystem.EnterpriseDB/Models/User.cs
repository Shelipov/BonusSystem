using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BonusSystem.EnterpriseDB.Models
{
    public class User
    {
        public int UserID { get; set; }
        [DataType("Email")]
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string UserPhoneNumber { get; set; }
        public DateTime LastChangedOn { get; set; }
    }
}

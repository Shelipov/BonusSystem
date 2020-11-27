using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BonusSystem.EnterpriseDB.DataTransferObjects
{
    public class UserDTO
    {
        public int? UserID { get; set; }
        [DataType("Email")]
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
    }
}

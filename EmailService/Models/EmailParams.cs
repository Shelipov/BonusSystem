using BonusSystem.EnterpriseDB.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Models
{
    public class EmailParams
    {
        public string Message { get; set; }

        public List<UserDTO> UserList { get; set; }

        public string Title { get; set; }
    }
}

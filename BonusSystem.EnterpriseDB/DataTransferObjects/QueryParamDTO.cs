using BonusSystem.EnterpriseDB.Repocitory.ReadModels.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BonusSystem.EnterpriseDB.DataTransferObjects
{
    public class QueryParamDTO 
    {
        [DataType(DataType.PhoneNumber)]
        public string UserPhoneNumber { get;  set; }
        public int? UserID { get;  set; }
        public int? ClientID { get;  set; }
        public int? BonusCardID { get;  set; }
        public string BonusCardNumber { get;  set; }
        public DateTime? BonusCardTimeEnd { get;  set; }
        public decimal? BonusCardBalanse { get;  set; }
        public string UserEmail { get; set; }
        public decimal? MoneyFromBonusCard { get; set; }
    }
}

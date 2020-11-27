using System;
using System.Collections.Generic;
using System.Text;

namespace BonusSystem.EnterpriseDB.Repocitory.ReadModels.Query
{
    public static class QueryCollection
    {
        public const string GetCardBalanseByCardNumber = @"select * from BonusCards as b where b.BonusCardNumber in @param";

        public const string GetCardBalanseByUser = @"select b.* from BonusCards as b
                                                      join Clients as c on c.BonusCardID=b.BonusCardID
                                                      join Users as u on u.UserID=c.UserID
                                                       where u.UserID in @param";
        public const string GetCardByNumberPhone = @"select b.* from BonusCards as b
                                                      join Clients as c on c.BonusCardID=b.BonusCardID
                                                      join Users as u on u.UserID=c.UserID
                                                       where u.UserPhoneMumber in @param";
    }
}

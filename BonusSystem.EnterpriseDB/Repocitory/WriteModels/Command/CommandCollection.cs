using System;
using System.Collections.Generic;
using System.Text;

namespace BonusSystem.EnterpriseDB.Repocitory.WriteModels.Command
{
    public static class CommandCollection
    {
        public const string MoneyFromBonusCard = @"  declare @BonusCardNumber nvarchar(150) = @param,
                                                              @BonusCardBalanse decimal = @money
                                                               update BonusCards
                                                               set BonusCardBalanse = BonusCardBalanse - @BonusCardBalanse,LastChangedOn=getdate()
                                                               where BonusCards.BonusCardNumber = @BonusCardNumber";

        public const string MoneyToBonusCard = @"  declare @BonusCardNumber nvarchar(150) = @param,
                                                              @BonusCardBalanse decimal = @money
                                                               update BonusCards
                                                               set BonusCardBalanse = BonusCardBalanse + @BonusCardBalanse,LastChangedOn=getdate()
                                                               where BonusCards.BonusCardNumber = @BonusCardNumber";

        public const string CreateBonusCard = @"    declare
                                                   @EndDade datetime = (SELECT DATEFROMPARTS((YEAR(GETDATE())+2), (SELECT MONTH(GETDATE())) , (SELECT DAY(GETDATE()))))  
                                                   Insert BonusCards (BonusCardNumber,BonusCardBalanse,BonusCardTimeEnd,LastChangedOn)
                                                   values (@param,0,@EndDade,getdate())";

        public const string MaxNumberBonusCard = @"Select MAX(b.BonusCardNumber) from BonusCards as b";
    }
}

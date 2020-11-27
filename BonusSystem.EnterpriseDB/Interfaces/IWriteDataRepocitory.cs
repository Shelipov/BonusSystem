using BonusSystem.EnterpriseDB.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BonusSystem.EnterpriseDB.Interfaces
{
    public interface IWriteDataRepocitory
    {
        Task MoneyFromBonusCard(QueryParamDTO query);
        Task MoneyToBonusCard(QueryParamDTO query);
        Task CreateBonusCard();
    }
}

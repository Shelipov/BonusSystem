using BonusSystem.EnterpriseDB.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.DataProviders.Interfaces
{
    public interface IDataProvider
    {
        Task<IEnumerable<dynamic>> GetCardByNumberPhone(IEnumerable<QueryParamDTO> query);
        Task<IEnumerable<dynamic>> GetCardBalanseByUser(IEnumerable<QueryParamDTO> query);
        Task<IEnumerable<dynamic>> GetCardBalanseByCardNumber(IEnumerable<QueryParamDTO> query);
        Task MoneyFromBonusCard(QueryParamDTO query);
        Task MoneyToBonusCard(QueryParamDTO query);
        Task CreateBonusCard();
    }
}

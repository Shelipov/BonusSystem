using BonusSystem.EnterpriseDB.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BonusSystem.EnterpriseDB.Interfaces
{
    public interface IReadDataRepocitory
    {
        Task<IEnumerable<dynamic>> GetCardByNumberPhone(IEnumerable<QueryParamDTO> query);
        Task<IEnumerable<dynamic>> GetCardBalanseByUser(IEnumerable<QueryParamDTO> query);
        Task<IEnumerable<dynamic>> GetCardBalanseByCardNumber(IEnumerable<QueryParamDTO> query);
        Task<IEnumerable<QueryParamDTO>> GetCards(QueryParamDTO query);
    }
}

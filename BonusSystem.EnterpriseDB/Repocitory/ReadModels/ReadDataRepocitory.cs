using BonusSystem.EnterpriseDB.DataTransferObjects;
using BonusSystem.EnterpriseDB.Interfaces;
using BonusSystem.EnterpriseDB.Repocitory.ReadModels.Query;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusSystem.EnterpriseDB.Repocitory.ReadModels
{
    public class ReadDataRepocitory: IReadDataRepocitory
    {
        Configuration.Configuration configuration;
        IDbConnection _enterprice;
        ILogger logger;
        const int TimeOut = 180000;
        public ReadDataRepocitory(Configuration.Configuration _configuration, ILogger<ReadDataRepocitory> _logger)
        {
            configuration = _configuration;
            _enterprice = new SqlConnection(configuration.EnterpriseDbContext);
            logger = _logger;
        }
        public async Task<IEnumerable<dynamic>> GetCardByNumberPhone(IEnumerable<QueryParamDTO> query)
        {
            logger.LogInformation($"Using method GetCardByNumberPhone whis params {query}");
            return await _enterprice.QueryAsync<dynamic>(QueryCollection.GetCardByNumberPhone, new { @param = query.Select(x => x.UserPhoneNumber) }, commandTimeout: TimeOut);
        }
        public async Task<IEnumerable<dynamic>> GetCardBalanseByUser(IEnumerable<QueryParamDTO> query)
        {
            logger.LogInformation($"Using method GetCardBalanseByUser whis params {query}");
            return await _enterprice.QueryAsync<dynamic>(QueryCollection.GetCardBalanseByUser, new { @param = query.Select(x => x.UserID) }, commandTimeout: TimeOut);
        }
        public async Task<IEnumerable<dynamic>> GetCardBalanseByCardNumber(IEnumerable<QueryParamDTO> query)
        {
            logger.LogInformation($"Using method GetCardBalanseByCardNumber whis params {query}");
            return await _enterprice.QueryAsync<dynamic>(QueryCollection.GetCardBalanseByCardNumber, new { @param = query.Select(x=>x.BonusCardNumber) }, commandTimeout: TimeOut);
        }
        public async Task<IEnumerable<QueryParamDTO>> GetCards(QueryParamDTO query)
        {
            logger.LogInformation($"Using method GetCards whis params {query}");
            return await _enterprice.QueryAsync<QueryParamDTO>(QueryCollection.GetCards, new { @UserPhoneNumber = query.UserPhoneNumber, @BonusCardNumber = query.BonusCardNumber }, commandTimeout: TimeOut);
        }
    }
}

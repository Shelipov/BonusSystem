using BonusSystem.Configuration;
using BonusSystem.EnterpriseDB.DataTransferObjects;
using BonusSystem.EnterpriseDB.Interfaces;
using BonusSystem.EnterpriseDB.Repocitory.WriteModels.Command;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BonusSystem.EnterpriseDB.Repocitory.WriteModels
{
    
    public class WriteDataRepocitory : IWriteDataRepocitory
    {
        Configuration.Configuration configuration;
        IDbConnection _enterprice;
        ILogger logger;
        const int TimeOut = 180000;
        const int LimitBonusCard = 999999;
        public WriteDataRepocitory(Configuration.Configuration _configuration, ILogger<WriteDataRepocitory> _logger)
        {
            configuration = _configuration;
            _enterprice = new SqlConnection(configuration.EnterpriseDbContext);
            logger = _logger;
        }

        public async Task MoneyFromBonusCard(QueryParamDTO query)
        {
            logger.LogInformation($"Using method MoneyFromBonusCard whis params {query}");
            await _enterprice.QueryAsync<dynamic>(CommandCollection.MoneyFromBonusCard, new { @param = query.BonusCardNumber,@money = query.MoneyFromBonusCard }, commandTimeout: TimeOut);
        }
        public async Task MoneyToBonusCard(QueryParamDTO query)
        {
            logger.LogInformation($"Using method MoneyToBonusCard whis params {query}");
            await _enterprice.QueryAsync<dynamic>(CommandCollection.MoneyToBonusCard, new { @param = query.BonusCardNumber, @money = query.MoneyFromBonusCard }, commandTimeout: TimeOut);
        }
        public async Task CreateBonusCard()
        {
            logger.LogInformation($"Using method CreateBonusCard ");
            var MaxNumberBonusCard = (await _enterprice.QueryAsync<int>(CommandCollection.MaxNumberBonusCard)).FirstOrDefault();
            if(MaxNumberBonusCard >= LimitBonusCard)
            {
                throw new Exception($"Лимит по созданию карт исчерпан обратитесь в службу поддержки");
            }

            await _enterprice.QueryAsync<dynamic>(CommandCollection.CreateBonusCard, new { @param = (MaxNumberBonusCard + 1).ToString("D6") }, commandTimeout: TimeOut);
        }
    }
}

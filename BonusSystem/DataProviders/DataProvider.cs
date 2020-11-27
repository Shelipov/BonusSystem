
using BonusSystem.DataProviders.Interfaces;
using BonusSystem.EnterpriseDB.DataTransferObjects;
using BonusSystem.EnterpriseDB.Interfaces;
using BonusSystem.EnterpriseDB.Models;
using ElasticSearch.Helpers;
using ElasticSearch.Interfaces;
using EmailService.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BonusSystem.DataProviders
{
    public class DataProvider: IDataProvider
    {
        ILogger<DataProvider> logger;
        IReadDataRepocitory read;
        IWriteDataRepocitory write;
        IEmailManager mail;
        IElasticSearch elastic;
        Configuration.Configuration Configuration;


        public DataProvider(ILogger<DataProvider> _logger
            ,IReadDataRepocitory _read
            , IWriteDataRepocitory _write, IEmailManager _mail, IElasticSearch _elastic)
        {
            elastic = _elastic;
            logger = _logger;
            read = _read;
            write = _write;
            mail = _mail;
            Configuration = new Configuration.Configuration();
        }
        public async Task<IEnumerable<dynamic>> GetCardByNumberPhone(IEnumerable<QueryParamDTO> query)
        {
            try
            {
                return await read.GetCardByNumberPhone(query);
            }
            catch(Exception ex)
            {
                await elastic.Execute(ex);
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                await mail.SendEmailAsync(new EmailService.Models.EmailParams() { Title = $"Exeption in File : {stack.GetFileName()}", UserList = new List<UserDTO>() { new UserDTO() { UserID = query.Select(x => x.UserID).FirstOrDefault(), UserEmail = Configuration.SmtpClientAdminEmail, UserFullName = Configuration.SmtpClientAdminEmail, UserName = Configuration.SmtpClientAdminEmail } } });
                throw new Exception($"Не удалось обработать запрос обратитесь за помощью в службу поддержки");
            }
        }
        public async Task<IEnumerable<dynamic>> GetCardBalanseByUser(IEnumerable<QueryParamDTO> query)
        {
            try
            {
                return await read.GetCardBalanseByUser(query);
            }
            catch (Exception ex)
            {
                await elastic.Execute(ex);
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                await mail.SendEmailAsync(new EmailService.Models.EmailParams() { Title = $"Exeption in File : {stack.GetFileName()}", UserList = new List<UserDTO>() { new UserDTO() { UserID = query.Select(x => x.UserID).FirstOrDefault(), UserEmail = Configuration.SmtpClientAdminEmail, UserFullName = Configuration.SmtpClientAdminEmail, UserName = Configuration.SmtpClientAdminEmail } } });
                throw new Exception($"Не удалось обработать запрос обратитесь за помощью в службу поддержки");
            }
        }
        public async Task<IEnumerable<dynamic>> GetCardBalanseByCardNumber(IEnumerable<QueryParamDTO> query)
        {
            try
            {
                return await read.GetCardBalanseByCardNumber(query);
            }
            catch (Exception ex)
            {
                await elastic.Execute(ex);
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                await mail.SendEmailAsync(new EmailService.Models.EmailParams() { Title = $"Exeption in File : {stack.GetFileName()}", UserList = new List<UserDTO>() { new UserDTO() { UserID = query.Select(x=>x.UserID).FirstOrDefault(), UserEmail = Configuration.SmtpClientAdminEmail, UserFullName = Configuration.SmtpClientAdminEmail, UserName = Configuration.SmtpClientAdminEmail } } });
                throw new Exception($"Не удалось обработать запрос обратитесь за помощью в службу поддержки");
            }
        }
        public async Task MoneyFromBonusCard(QueryParamDTO query)
        {
            try
            {
                var card = (await read.GetCardBalanseByCardNumber(new List<QueryParamDTO>(){ query })).Select(x => (IDictionary<string, object>)x).ToList().FirstOrDefault();
                if (card == null)
                {
                    await elastic.Execute(TypeEvent.Warning, $"По карте {query.BonusCardNumber} : Нет информации в базе данных");
                    throw new Exception($"По карте {query.BonusCardNumber} : Нет информации в базе данных");
                }
                if ((DateTime)card.Where(x => x.Key == "BonusCardTimeEnd").Select(x => x.Value).FirstOrDefault() < DateTime.Now)
                {
                    await elastic.Execute(TypeEvent.Warning, $"По карте {query.BonusCardNumber} : Истек срок действия карты");
                    throw new Exception($"По карте {query.BonusCardNumber} : Истек срок действия карты");
                }

                if ((decimal)card.Where(x => x.Key == "BonusCardBalanse").Select(x => x.Value).FirstOrDefault() < (query.MoneyFromBonusCard??0))
                {
                        await elastic.Execute(TypeEvent.Warning, $"По карте {query.BonusCardNumber} :Не достаточно средств для списания");
                        throw new Exception($"По карте {query.BonusCardNumber} :Не достаточно средств для списания");
                }
                await write.MoneyFromBonusCard(query);
            }
            catch (Exception ex)
            {
                await elastic.Execute(ex);
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                await mail.SendEmailAsync(new EmailService.Models.EmailParams() { Title = $"Exeption in File : {stack.GetFileName()}", UserList = new List<UserDTO>() { new UserDTO() { UserID = query.UserID, UserEmail = Configuration.SmtpClientAdminEmail, UserFullName = Configuration.SmtpClientAdminEmail, UserName = Configuration.SmtpClientAdminEmail } } });
                throw new Exception(ex.Message);
            }
        }
        public async Task MoneyToBonusCard(QueryParamDTO query)
        {
            try
            {
                var card = (await read.GetCardBalanseByCardNumber(new List<QueryParamDTO>() { query })).Select(x => (IDictionary<string, object>)x).ToList().FirstOrDefault();
                if(card == null)
                {
                    await elastic.Execute(TypeEvent.Warning, $"По карте {query.BonusCardNumber} : Нет информации в базе данных");
                    throw new Exception($"По карте {query.BonusCardNumber} : Нет информации в базе данных");
                }
                if ((DateTime)card.Where(x => x.Key == "BonusCardTimeEnd").Select(x => x.Value).FirstOrDefault() < DateTime.Now)
                {
                    await elastic.Execute(TypeEvent.Warning, $"По карте {query.BonusCardNumber} : Истек срок действия карты");
                    throw new Exception($"По карте {query.BonusCardNumber} : Истек срок действия карты");
                }

                if ((decimal)card.Where(x => x.Key == "BonusCardBalanse").Select(x => x.Value).FirstOrDefault() < (query.MoneyFromBonusCard ?? 0))
                {
                    await elastic.Execute(TypeEvent.Warning, $"По карте {query.BonusCardNumber} :Не достаточно средств для списания");
                    throw new Exception($"По карте {query.BonusCardNumber} :Не достаточно средств для списания");
                }
                await write.MoneyToBonusCard(query);
            }
            catch (Exception ex)
            {
                await elastic.Execute(ex);
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                await mail.SendEmailAsync(new EmailService.Models.EmailParams() { Title = $"Exeption in File : {stack.GetFileName()}", UserList = new List<UserDTO>() { new UserDTO() { UserID = query.UserID, UserEmail = Configuration.SmtpClientAdminEmail, UserFullName = Configuration.SmtpClientAdminEmail, UserName = Configuration.SmtpClientAdminEmail } } });
                throw new Exception(ex.Message);
            }
        }
        public async Task CreateBonusCard()
        {
            try
            {
                await write.CreateBonusCard();
            }
            catch (Exception ex)
            {
                await elastic.Execute(ex);
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                await mail.SendEmailAsync(new EmailService.Models.EmailParams() { Title = $"Exeption in File : {stack.GetFileName()}", UserList = new List<UserDTO>() { new UserDTO() { UserID = 0, UserEmail = Configuration.SmtpClientAdminEmail, UserFullName = Configuration.SmtpClientAdminEmail, UserName = Configuration.SmtpClientAdminEmail } } });
                throw new Exception(ex.Message);
            }
        }
    }
}

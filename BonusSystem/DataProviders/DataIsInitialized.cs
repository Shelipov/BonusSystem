using BonusSystem.DataProviders.Interfaces;
using BonusSystem.EnterpriseDB.Models;
using BonusSystem.EnterpriseDB.Models.EnterpriseDB;
using BonusSystem.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.DataProviders
{
    
    public class DataIsInitialized: IDataIsInitialized , IDesignTimeDbContextFactory<EnterpriseDBContext>
    {
        EnterpriseDBContext context;
        Configuration.Configuration Configuration;
        public DataIsInitialized(string[] args)
        {
            Configuration = new Configuration.Configuration();
            context = CreateDbContext(args);
        }

        public EnterpriseDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EnterpriseDBContext>();
            optionsBuilder.UseSqlServer(Configuration.EnterpriseDbContext, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new EnterpriseDBContext(optionsBuilder.Options);
        }

        public async Task Execute()
        {
            if (context.Users.Count() < 1)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Uses = new List<User>();
                        var Clients = new List<Client>();
                        var BonusCards = new List<BonusCard>();
                        var random = new Random();
                        for (int i = 1; i < 1001; i++)
                        {
                            Uses.Add(new User()
                            {
                                UserName = $"{Guid.NewGuid()}",
                                UserFullName = $"{GenerateUserName.GenerateRandomUsername()}",
                                UserPhoneNumber = $"{GeneratePhoneNumber.GenerateRandomPhoneNumber()}",
                                UserEmail = $"{GenerateUserName.GenerateRandomUsername()}@gmail.com",
                                LastChangedOn = DateTime.Now
                            });
                            BonusCards.Add(new BonusCard()
                            {
                                BonusCardNumber = i.ToString("D6"),
                                BonusCardBalanse = random.Next(0, 100000),
                                BonusCardTimeEnd = DateTime.Now.AddYears(2),
                                LastChangedOn = DateTime.Now
                            });
                            Clients.Add(new Client()
                            {
                                User = Uses[i - 1],
                                BonusCard = BonusCards[i - 1],
                                LastChangedOn = DateTime.Now
                            });
                        }

                        await context.AddRangeAsync(BonusCards);
                        await context.Users.AddRangeAsync(Uses);
                        await context.Clients.AddRangeAsync(Clients);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

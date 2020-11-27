using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonusSystem.EnterpriseDB.Models.EnterpriseDB
{
    public class EnterpriseDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BonusCard> BonusCards { get; set; }

        public EnterpriseDBContext(DbContextOptions<EnterpriseDBContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

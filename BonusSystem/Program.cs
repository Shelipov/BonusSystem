using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BonusSystem.DataProviders;
using BonusSystem.DataProviders.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BonusSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() => new DataIsInitialized(args).Execute());
            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        
    }
}

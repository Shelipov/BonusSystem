using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonusSystem.DataProviders;
using BonusSystem.DataProviders.Interfaces;
using BonusSystem.EnterpriseDB.Interfaces;
using BonusSystem.EnterpriseDB.Models.EnterpriseDB;
using BonusSystem.EnterpriseDB.Repocitory.ReadModels;
using BonusSystem.EnterpriseDB.Repocitory.WriteModels;
using ElasticSearch.Interfaces;
using EmailService;
using EmailService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BonusSystem
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new Configuration.Configuration();
        }

        public Configuration.Configuration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Configuration.Configuration>();
            services.AddDbContext<EnterpriseDBContext>(options =>
                options.UseSqlServer(Configuration.EnterpriseDbContext));
            services.AddTransient<IReadDataRepocitory, ReadDataRepocitory>();
            services.AddTransient<IEmailManager, EmailManager>();
            services.AddTransient<IDataProvider, DataProvider>();
            services.AddTransient<IWriteDataRepocitory, WriteDataRepocitory>();
            services.AddTransient<IElasticSearch, ElasticSearch.ElasticSearch>();


            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("BonusSystemPolicy",
                    builder => builder
                    .WithOrigins("https://example.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());
            });
            services.AddSession();
            services.AddRouting();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseResponseCompression();

            app.UseCors("BonusSystemPolicy");

            app.Use(async (context, next) =>
            {
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();

            loggerFactory.AddDebug(minLevel: LogLevel.Error);
            loggerFactory.AddDebug(minLevel: LogLevel.Warning);
            loggerFactory.AddConsole(minLevel: LogLevel.Warning);
            loggerFactory.AddConsole(minLevel: LogLevel.Error);
            loggerFactory.AddConsole(includeScopes: true);
        }
    }
}

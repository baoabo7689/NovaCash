using System;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using NovaCash.Sportsbook.Clients.Configurations;
using NovaCash.SportsbookWebServices.HealthChecks;
using NovaCash.SportsbookWebServices.Wokers;

namespace NovaCash.SportsbookWebServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddHealthChecks()
                .AddCheck<BetDetailServiceHealthCheck>("BetDetailServiceHealthCheck",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "example" });

            var storage = new MySqlStorage(AppSettings.Settings.HangfireConnection, new MySqlStorageOptions
            {
                TransactionIsolationLevel = System.Data.IsolationLevel.ReadCommitted,
                QueuePollInterval = TimeSpan.FromSeconds(15),
                JobExpirationCheckInterval = TimeSpan.FromHours(1),
                CountersAggregateInterval = TimeSpan.FromMinutes(5),
                PrepareSchemaIfNecessary = true,
                DashboardJobListLimit = 50000,
                TransactionTimeout = TimeSpan.FromMinutes(1),
                TablePrefix = "Hangfire"
            });

            services.AddHangfire(configuration => configuration.UseStorage(storage));
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = BetDetailServiceHealthCheck.WriteResponse
                });
            });

            app.UseHangfireDashboard();
            new BetDetailWorker().Run();
        }
    }
}
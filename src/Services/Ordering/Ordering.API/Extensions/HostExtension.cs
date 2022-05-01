using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Ordering.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailablity = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TContext>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    InvokeSeeder(seeder, context, services);
                    logger.LogInformation("Migrating Ordering API database data");
                    
                }
                catch (SqlException ex)
                {

                    logger.LogError(ex, "An error occurred");

                    if (retryForAvailablity < 50)
                    {
                        retryForAvailablity++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailablity);
                    }
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}

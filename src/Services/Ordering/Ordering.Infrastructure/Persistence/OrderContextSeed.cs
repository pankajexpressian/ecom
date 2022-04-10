using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                await orderContext.Orders.AddRangeAsync(GetSeedingData());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Orders Seeded");
            }
        }

        private static IEnumerable<Domain.Entities.Order> GetSeedingData()
        {
            var ordersToSeed = new List<Domain.Entities.Order>();

            ordersToSeed.Add(new Domain.Entities.Order()
            {
                UserName = "pankaj",
                FirstName = "Pankaj",
                LastName = "Jangid",
                EmailAddress = "pankajexpressian92@gmail.com",
            });

            return ordersToSeed;
        }
    }
}

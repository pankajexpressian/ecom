

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options):base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = System.DateTime.Now;
                        entry.Entity.CreatedBy = "Pankaj";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = System.DateTime.Now;
                        entry.Entity.UpdatedBy = "Pankaj";
                        break;
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

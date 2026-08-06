using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Billing.Infra.Persistence.Factory
{
    public class BillingDbContextFactory:IDesignTimeDbContextFactory<BillingDbContext>
    {

        public BillingDbContext CreateDbContext(string[] args)
        {
            var oprions = new DbContextOptionsBuilder<BillingDbContext>()
                               .UseNpgsql("Host=localhost;Database=billing_design;Username=postgres;Password=postgres")
                               .UseSnakeCaseNamingConvention()
                               .Options;

            return new BillingDbContext(oprions);

        }

    }
}

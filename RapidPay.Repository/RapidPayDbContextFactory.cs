using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RapidPay.Repository
{
    public class RapidPayDbContextFactory : IDesignTimeDbContextFactory<RapidPayDbContext>
    {
        public RapidPayDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RapidPayDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=RapidPayDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new RapidPayDbContext(optionsBuilder.Options);
        }
    }
}
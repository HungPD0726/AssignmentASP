using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ASPAssignment.DataAccess.Context
{
    public class FuHouseFinderContextFactory : IDesignTimeDbContextFactory<FuHouseFinderContext>
    {
        public FuHouseFinderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FuHouseFinderContext>();

            // Hard-code connection string for design-time
            optionsBuilder.UseSqlServer("Server=.;Database=FuHouseFinderDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new FuHouseFinderContext(optionsBuilder.Options);
        }
    }
}

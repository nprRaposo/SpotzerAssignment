using Microsoft.EntityFrameworkCore;
using SpotzerAssignment.Model;

namespace SpotzerAssignment.Data
{
    public class SpotzerContext : DbContext
    {
        public SpotzerContext(DbContextOptions<SpotzerContext> options)
           : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<PaidSearchProductLine> PaidSearchProductLine { get; set; }
        public DbSet<WebSiteProductLine> WebSiteProductLine { get; set; }
    }
}

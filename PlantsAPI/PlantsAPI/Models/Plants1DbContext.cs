using Microsoft.EntityFrameworkCore;

namespace PlantsAPI.Models
{
    public class Plants1DbContext : DbContext
    {
        public Plants1DbContext(DbContextOptions<Plants1DbContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }

    }
}

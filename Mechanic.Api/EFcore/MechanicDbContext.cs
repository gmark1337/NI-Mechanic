using Microsoft.EntityFrameworkCore;

namespace Mechanic
{
    public class MechanicDbContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }
        public MechanicDbContext(DbContextOptions options)
            : base(options)
        {

        }

    }
}

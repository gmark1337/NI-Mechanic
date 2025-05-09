using Microsoft.EntityFrameworkCore;

using Mechanic.Shared;

namespace Mechanic.EFcore;

public class MechanicDbContext : DbContext
{
    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }
    public MechanicDbContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Job>()
            .HasOne(c => c.Client)
            .WithMany(c => c.Jobs)
            .HasForeignKey(c => c.customerId);
    }

}

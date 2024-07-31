using AllHandsMaio2024.Entities;
using Microsoft.EntityFrameworkCore;

namespace AllHandsMaio2024.Repositories.EF.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

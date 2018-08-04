using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeDomain>().ToTable("Attributes").HasKey(x => x.Id);
            modelBuilder.Entity<EntityDomain>().ToTable("Entities").HasKey(x => x.Id);
            modelBuilder.Entity<DataTypeDomain>().ToTable("DataTypes").HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\v11.0;Database=DynamicRestApi;Trusted_Connection=true;");
        }
    }
}

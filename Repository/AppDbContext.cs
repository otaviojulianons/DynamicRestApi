using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repository
{
    public class AppDbContext : DbContext
    {

        public DbSet<EntityDomain> Entities { get; set; }
        public DbSet<AttributeDomain> Attributes { get; set; }
        public DbSet<DataTypeDomain> DataTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataTypeDomain>()
                .ToTable("DataTypes")
                .HasKey(x => x.Id);


            modelBuilder.Entity<AttributeDomain>()
                .ToTable("Attributes")
                .HasKey(x => x.Id);


            modelBuilder.Entity<EntityDomain>()
                .ToTable("Entities")
                .HasKey(x => x.Id);

            modelBuilder.Entity<EntityDomain>()
              .HasMany(c => c.Attributes)
              .WithOne(e => e.Entity);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\v11.0;Database=DynamicRestApi;Trusted_Connection=true;");
        }
    }
}

using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<EntityDomain> Entities { get; set; }
        public DbSet<AttributeDomain> Attributes { get; set; }
        public DbSet<DataTypeDomain> DataTypes { get; set; }
        public DbSet<LanguageDomain> Languages { get; set; }
        public DbSet<LanguageDataTypeDomain> LanguagesDataTypes { get; set; }

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

            modelBuilder.Entity<LanguageDomain>()
                .ToTable("Languages")
                .HasKey(x => x.Id);

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .ToTable("LanguagesDataTypes")
                .HasKey(x => x.Id);

            modelBuilder.Entity<EntityDomain>()
              .HasMany(c => c.Attributes)
              .WithOne(e => e.Entity);

            modelBuilder.Entity<LanguageDomain>()
              .HasMany(c => c.DataTypes)
              .WithOne(e => e.Language);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ojns;Database=DynamicRestApi;Trusted_Connection=true;");
        }

        public void Drop(string name)
        {
            Database.ExecuteSqlCommand((string)$"drop table dbo.{name}");
        }
    }
}

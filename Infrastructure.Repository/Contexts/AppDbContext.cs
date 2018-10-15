using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Contexts
{
    public class AppDbContext : DbContext , IDatabaseService
    {

        public DbSet<EntityDomain> Entities { get; set; }
        public DbSet<AttributeDomain> Attributes { get; set; }
        public DbSet<DataTypeDomain> DataTypes { get; set; }
        public DbSet<LanguageDomain> Languages { get; set; }
        public DbSet<LanguageDataTypeDomain> LanguagesDataTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DataType

            modelBuilder.Entity<DataTypeDomain>()
                .ToTable("DataTypes")
                .HasKey(x => x.Id);

            #endregion

            #region Attribute

            modelBuilder.Entity<AttributeDomain>()
                .ToTable("Attributes")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AttributeDomain>()
                .Property<long>("EntityId");

            modelBuilder.Entity<AttributeDomain>()
                .Property<long>("DataTypeId");


            #endregion

            #region Entity

            modelBuilder.Entity<EntityDomain>().Metadata.FindNavigation(nameof(EntityDomain.Attributes)).SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<EntityDomain>()
                .ToTable("Entities")
                .HasKey(x => x.Id);

            modelBuilder.Entity<EntityDomain>()
              .HasMany(c => c.Attributes)
              .WithOne(e => e.Entity)
              .HasForeignKey("EntityId")
              .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region Language

            modelBuilder.Entity<LanguageDomain>()
                .ToTable("Languages")
                .HasKey(x => x.Id);

            modelBuilder.Entity<LanguageDomain>()
              .HasMany(c => c.DataTypes)
              .WithOne(e => e.Language);

            #endregion

            #region Language DataType

            modelBuilder.Entity<LanguageDataTypeDomain>()
                    .ToTable("LanguagesDataTypes")
                    .HasKey(x => x.Id);

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .Property<long>("LanguageId");

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .Property<long>("DataTypeId");

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ojns;Database=DynamicRestApi;Trusted_Connection=true;");
        }

        public void DropEntity(string name)
        {
            Database.ExecuteSqlCommand((string)$"drop table dbo.{name}");
        }

    }
}

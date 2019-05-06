using Domain.Core.ValueObjects;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Data.Repository.Contexts
{
    public class DbContextSqlServer : DbContext
    {
        private readonly string _connectionString;

        public DbContextSqlServer(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("Database:SQLServer:ConnectionString");
        }

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

            modelBuilder.Entity<DataTypeDomain>()
                .Property(x => x.Name)
                .HasConversion( name => name.Value, value => new Name(value));

            #endregion

            #region Attribute

            modelBuilder.Entity<AttributeDomain>()
                .ToTable("Attributes")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AttributeDomain>()
                .Property<Guid>("EntityId");

            modelBuilder.Entity<AttributeDomain>()
                .Property<Guid>("DataTypeId");

            modelBuilder.Entity<AttributeDomain>()
                .Property(x => x.Name)
                .HasConversion(name => name.Value, value => new Name(value));


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

            modelBuilder.Entity<EntityDomain>()
                .Property(x => x.Name)
                .HasConversion(name => name.Value, value => new Name(value));


            #endregion

            #region Language

            modelBuilder.Entity<LanguageDomain>()
                .ToTable("Languages")
                .HasKey(x => x.Id);

            modelBuilder.Entity<LanguageDomain>()
                .HasMany(c => c.DataTypes)
                .WithOne(e => e.Language);

            modelBuilder.Entity<LanguageDomain>()
                .Property(x => x.Name)
                .HasConversion(name => name.Value, value => new Name(value));

            #endregion

            #region Language DataType

            modelBuilder.Entity<LanguageDataTypeDomain>()
                    .ToTable("LanguagesDataTypes")
                    .HasKey(x => x.Id);

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .Property<Guid>("LanguageId");

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .Property<Guid>("DataTypeId");

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .Property(x => x.Name)
                .HasConversion(name => name.Value, value => new Name(value));

            modelBuilder.Entity<LanguageDataTypeDomain>()
                .Property(x => x.NameNullable)
                .HasConversion(name => name.Value, value => new Name(value));

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

﻿using Domain.Core.ValueObjects;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Attribute

            modelBuilder.Entity<AttributeDomain>()
                .ToTable("Attributes")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AttributeDomain>()
                .Property<Guid>("EntityId");

            modelBuilder.Entity<AttributeDomain>()
                .Property(x => x.Name)
                .HasConversion(name => name.Value, value => new Name(value));
            
            modelBuilder.Entity<AttributeDomain>()
                .Property(x => x.DataType)
                .HasConversion(datatype => datatype.ToString(), value => Enum.Parse<EnumDataTypes>(value));

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

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
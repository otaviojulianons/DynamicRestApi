using Domain.Core.Interfaces.Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace Infrastructure.Repository.Contexts
{
    public class DynamicDbContext<T> : DbContext where T : class, IEntity
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().ToTable(typeof(T).Name).HasKey( x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer("Server=ojns;Database=DynamicRestApi;Trusted_Connection=true;");  
        }

        public void CreateEntity()
        {
            try
            {
                this.Set<T>().Count();
            }
            catch (Exception)
            {
                RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)this.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
            }
        }
    }
}

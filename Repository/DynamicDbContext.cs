using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SharedKernel.Repository;
using System;
using System.Linq;

namespace Repository
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
            optionsBuilder.UseSqlServer("Server=(localdb)\\v11.0;Database=DynamicRestApi;Trusted_Connection=true;");  
        }

        public void Create()
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

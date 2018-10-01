using Domain.Interfaces.Structure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Base
{
    public class Repository<T> : IRepository<T> where T :  class, IEntity
    {
        private DbContext _context;
        public DbSet<T> DbSet { get; set; }

        public Repository(DbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Queryble()
        {
            return DbSet.OrderBy(x => x.Id);
        }

        public virtual void Update( T entity, bool commit = false)
        {
            DbSet.Update(entity);
            if (commit)
                Commit();
        }

        public virtual void Delete(long id, bool commit = false)
        {
            DbSet.Remove(DbSet.Where(x => x.Id.Equals(id)).FirstOrDefault());
            if (commit)
                Commit();
        }

        public virtual void Insert(T entity, bool commit = false)
        {
            DbSet.Add(entity);
            if (commit)
                Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Queryble();
        }

        public virtual IQueryable<T> QueryBy(Expression<Func<T, bool>> filtro = null)
        {
            return filtro != null ? Queryble().Where(filtro) : Queryble();
        }

        public virtual IQueryable<T> QueryById(long id)
        {
            return Queryble().Where( x => x.Id == id);
        }

        public virtual T GetById(long id)
        {
            return Queryble().FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}

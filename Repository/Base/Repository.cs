using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Base
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
        public void Update(long id, T entity, bool commit = false)
        {
            entity.Id = id;
            DbSet.Update(entity);
            if (commit)
                Commit();
        }

        public void Delete(long id, bool commit = false)
        {
            DbSet.Remove(DbSet.Where(x => x.Id.Equals(id)).FirstOrDefault());
            if (commit)
                Commit();
        }

        public void Insert(T entity, bool commit = false)
        {
            DbSet.Add(entity);
            if (commit)
                Commit();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.OrderBy( x => x.Id).ToList();
        }

        public IQueryable<T> QueryBy(Expression<Func<T, bool>> filtro = null)
        {
            return filtro != null ? DbSet.Where(filtro) : DbSet.AsQueryable();
        }

        public IQueryable<T> QueryById(long id)
        {
            return DbSet.Where( x => x.Id == id);
        }

        public T GetById(long id)
        {
            return DbSet.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}

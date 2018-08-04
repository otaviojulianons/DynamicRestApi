using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharedKernel.Repository
{
    public class Repository<T> : IRepository<T> where T :  class, IEntity
    {
        private DbContext _context;
        private DbSet<T> _set;

        public Repository(DbContext context)
        {
            _context = context;
           _set = context.Set<T>();
        }
        public void Update(T entity, bool commit = true)
        {
            _set.Update(entity);
            if (commit)
                Commit();
        }

        public void Delete(long id, bool commit = true)
        {
            _set.Remove(_set.Where(x => x.Id.Equals(id)).FirstOrDefault());
            if (commit)
                Commit();
        }

        public void Insert(T entity, bool commit = true)
        {
            _set.Add(entity);
            if (commit)
                Commit();
        }

        public IEnumerable<T> GetAll()
        {
            return _set.OrderBy( x => x.Id).ToList();
        }

        public IQueryable<T> QueryBy(Expression<Func<T, bool>> filtro = null)
        {
            return filtro != null ? _set.Where(filtro) : _set.AsQueryable();
        }

        public T GetById(long id)
        {
            return _set.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}

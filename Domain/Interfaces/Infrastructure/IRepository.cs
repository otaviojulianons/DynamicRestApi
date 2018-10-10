using Domain.Interfaces.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces.Infrastructure
{

    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> Queryble();

        T GetById(long id);

        IEnumerable<T> GetAll();

        IQueryable<T> QueryBy(Expression<Func<T, bool>> filter = null);

        IQueryable<T> QueryById(long id);

        void Insert(T entity, bool commit = true);

        void Update(T entity, bool commit = true);

        void Delete(long id,bool commit = true);

        void Commit();
    }
}

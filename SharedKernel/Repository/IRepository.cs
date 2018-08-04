using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharedKernel.Repository
{

    public interface IRepository<T> where T : IEntity
    {
        T GetById(long id);

        IEnumerable<T> GetAll();

        IQueryable<T> QueryBy(Expression<Func<T, bool>> filter);

        void Insert(T entity, bool commit = true);

        void Update(T entity, bool commit = true);

        void Delete(long id,bool commit = true);

        void Commit();

    }
}

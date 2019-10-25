using Domain.Core.Interfaces.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Core.Interfaces.Infrastructure
{

    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> Queryble();

        IEnumerable<T> GetAll();

        T GetById(Guid id);

        IQueryable<T> QueryBy(Expression<Func<T, bool>> filter = null);

        IQueryable<T> QueryById(Guid id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(Guid id);

    }
}

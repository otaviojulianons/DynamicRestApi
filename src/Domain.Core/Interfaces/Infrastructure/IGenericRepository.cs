using Domain.Core.Interfaces.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Core.Interfaces.Infrastructure
{

    public interface IGenericRepository<TEntity,TId> where TEntity : IGenericEntity<TId>
    {
        IQueryable<TEntity> Queryble();

        IEnumerable<TEntity> GetAll();

        TEntity GetById(TId id);

        IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> filter = null);

        IQueryable<TEntity> QueryById(TId id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TId id);

    }
}

using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Extensions;
using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Core.Implementation.Events;

namespace Infrastructure.Data.Repository.Repositories.Bases
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private IMediator _mediator;
        private INotificationManager _msg;


        public BaseRepository(IMediator mediator, INotificationManager msg)
        {
            _mediator = mediator;
            _msg = msg;
        }


        public virtual void Update( T entity)
        {
            try
            {
                if (entity is ISelfValidation)
                {
                    var validable = entity as ISelfValidation;
                    if (!validable.IsValid(_msg))
                        return;
                }

                UpdateEntity(entity);

                _mediator.Publish(new AfterUpdateEntityEvent<T>(entity));
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public abstract void UpdateEntity(T entity);

        public virtual void Delete(Guid id)
        {
            try
            {
                var entity = QueryBy(x => x.Id.Equals(id)).FirstOrDefault()
                    ?? throw new Exception("Object not fount.");

                DeleteEntity(entity);

                _mediator.Publish(new AfterDeleteEntityEvent<T>(entity));
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public abstract void DeleteEntity(T entity);

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity is ISelfValidation)
                {
                    var validable = entity as ISelfValidation;
                    if (!validable.IsValid(_msg))
                        return;
                }

                InsertEntity(entity);

                _mediator.Publish(new AfterInsertEntityEvent<T>(entity));

            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public abstract void InsertEntity(T entity);

        public virtual IEnumerable<T> GetAll() => Queryble();

        public virtual IQueryable<T> QueryBy(Expression<Func<T, bool>> filter = null) =>
            filter != null ? Queryble().Where(filter) : Queryble();

        public virtual IQueryable<T> QueryById(Guid id) =>
            Queryble().Where( x => x.Id == id);

        public virtual T GetById(Guid id) =>
            Queryble().FirstOrDefault(x => x.Id.Equals(id));

        public abstract IQueryable<T> Queryble();

    }
}

using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Common.Notifications;
using Infrastructure.Repository.Contexts;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Repositories
{
    public class MongodbGenericRepository<TEntity, TId> : 
        IGenericRepository<TEntity, TId> where TEntity : class, IGenericEntity<TId>
    {
        private IMongoDatabase _database;
        private IMediator _mediator;
        private INotificationManager _msg;
        protected IMongoCollection<TEntity> _collection;

        public MongodbGenericRepository(MongodbContext context, IMediator mediator, INotificationManager msg)
        {
            _mediator = mediator;
            _msg = msg;
            _database = context.Database;
            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual IQueryable<TEntity> Queryble() => _collection.AsQueryable();

        public virtual IEnumerable<TEntity> GetAll() => Queryble();

        public virtual IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> filter = null) =>
            filter != null ? Queryble().Where(filter) : Queryble();

        public virtual IQueryable<TEntity> QueryById(TId id) =>
            Queryble().Where(x => Equals(x.Id, id));

        public virtual TEntity GetById(TId id) =>
            Queryble().FirstOrDefault(x => x.Id.Equals(id));

        public virtual void Insert(TEntity entity)
        {
            try
            {
                _collection.InsertOne(entity);
                //_mediator.Publish(new EntityInsertedDomaiEvent<TEntity>(entity));

            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _collection.ReplaceOne(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity);
                //_mediator.Publish(new EntityUpdatedDomainEvent<TEntity>(entity));
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public virtual void Delete(TId id)
        {
            try
            {
                var entity = QueryBy(x => x.Id.Equals(id)).FirstOrDefault()
                    ?? throw new Exception("Object not fount.");

                _collection.DeleteOne(Builders<TEntity>.Filter.Eq("_id", id));
                //_mediator.Publish(new EntityDeletedDomainEvent<T>(entity));
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

    }
}

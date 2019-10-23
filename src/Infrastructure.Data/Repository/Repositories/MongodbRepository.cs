using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Infrastructure.CrossCutting.Notifications;
using Infrastructure.Data.Repository.Contexts;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repository.Repositories
{
    public class MongodbRepository<T> : IRepository<T> where T : class, IEntity
    {
        private IMongoDatabase _database;
        private IMediator _mediator;
        private INotificationManager _msg;
        protected IMongoCollection<T> _collection;

        public MongodbRepository(ContextMongodb context, IMediator mediator, INotificationManager msg)
        {
            _mediator = mediator;
            _msg = msg;
            _database = context.Database;
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        public virtual IQueryable<T> Queryble() => _collection.AsQueryable<T>();

        public virtual IEnumerable<T> GetAll() => Queryble();

        public virtual IQueryable<T> QueryBy(Expression<Func<T, bool>> filter = null) =>
            filter != null ? Queryble().Where(filter) : Queryble();

        public virtual IQueryable<T> QueryById(Guid id) =>
            Queryble().Where(x => x.Id == id);

        public virtual T GetById(Guid id) =>
            Queryble().FirstOrDefault(x => x.Id.Equals(id));

        public virtual void Insert(T entity)
        {
            try
            {
                _collection.InsertOne(entity);
                _mediator.Publish(new AfterInsertEntityEvent<T>(entity));

            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public virtual void Update( T entity)
        {
            try
            {
                _collection.ReplaceOne(Builders<T>.Filter.Eq("_id", entity.Id), entity);
                _mediator.Publish(new AfterUpdateEntityEvent<T>(entity));
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

        public virtual void Delete(Guid id)
        {
            try
            {
                var entity = QueryBy(x => x.Id.Equals(id)).FirstOrDefault()
                    ?? throw new Exception("Object not fount.");

                _collection.DeleteOne(Builders<T>.Filter.Eq("_id", id));
                _mediator.Publish(new AfterDeleteEntityEvent<T>(entity));
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new NotificationMessage(ex.Message));
            }
        }

    }
}

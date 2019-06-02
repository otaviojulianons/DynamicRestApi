using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Structure;
using Infrastructure.Data.Repository.Contexts;
using MediatR;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Data.Repository.Repositories.Bases
{
    public class BaseRepositoryMongodb<T> : BaseRepository<T> where T :  class, IEntity
    {
        private IMongoDatabase _database;
        private IMediator _mediator;
        private INotificationManager _msg;
        protected IMongoCollection<T> _collection { get; set; }

        public BaseRepositoryMongodb(ContextMongodb context, IMediator mediator, INotificationManager msg)
            : base(mediator, msg)
        {
            _database = context.Database;
            _collection = _database.GetCollection<T>(typeof(T).Name);
            _mediator = mediator;
            _msg = msg;
        }

        public override IQueryable<T> Queryble()
        {
            return _collection.AsQueryable<T>();
        }

        public override void UpdateEntity(T entity)
        {
            _collection.ReplaceOne(Builders<T>.Filter.Eq("_id", entity.Id), entity);
        }

        public override void DeleteEntity(T entity)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", entity.Id));
        }

        public override void InsertEntity(T entity)
        {
            _collection.InsertOne(entity);
        }
    }
}

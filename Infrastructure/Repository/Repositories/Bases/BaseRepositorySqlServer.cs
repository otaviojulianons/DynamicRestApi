using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Structure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data.Repository.Repositories.Bases
{
    public class BaseRepositorySqlServer<T> : BaseRepository<T> where T :  class, IEntity
    {
        private DbContext _context;
        private IMediator _mediator;
        private INotificationManager _msg;

        public DbSet<T> DbSet { get; set; }

        public BaseRepositorySqlServer(DbContext context, IMediator mediator, INotificationManager msg)
            : base(mediator,msg)
        {
            _context = context;
            _mediator = mediator;
            _msg = msg;
            DbSet = context.Set<T>();
        }

        public override IQueryable<T> Queryble()
        {
            return DbSet.OrderBy(x => x.Id);
        }

        public override void UpdateEntity( T entity)
        {
            DbSet.Update(entity);
            _context.SaveChanges();
        }

        public override void DeleteEntity(T entity)
        {
            DbSet.Remove(entity);
            _context.SaveChanges();
        }

        public override void InsertEntity(T entity)
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

    }
}

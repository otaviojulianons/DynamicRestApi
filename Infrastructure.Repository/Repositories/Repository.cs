using Domain.Core.Extensions;
using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T :  class, IEntity
    {
        private DbContext _context;
        private IMediator _mediator;
        private IMsgManager _msg;

        public DbSet<T> DbSet { get; set; }

        public Repository(DbContext context, IMediator mediator, IMsgManager msg)
        {
            _context = context;
            _mediator = mediator;
            _msg = msg;
            DbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Queryble()
        {
            return DbSet.OrderBy(x => x.Id);
        }

        public virtual void Update( T entity, bool commit = false)
        {
            try
            {
                if (entity is ISelfValidation<T>)
                {
                    var validation = entity as ISelfValidation<T>;
                    if (!validation.IsValid(_msg))
                        return;
                }

                DbSet.Update(entity);
                if (commit)
                    Commit();

                if (entity is IAggregateRoot)
                {
                    var aggregateRoot = entity as IAggregateRoot;
                    foreach (var notification in aggregateRoot.Notifications.Where(x => x is IAfterUpdateDomainEvent<T>))
                        _mediator.Publish(notification);
                }
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new Msg(ex.Message));
            }
        }

        public virtual void Delete(long id, bool commit = false)
        {
            try
            {
                var entity = QueryBy(x => x.Id.Equals(id)).FirstOrDefault()
                    ?? throw new Exception("Object not fount.");

                DbSet.Remove(entity);
                if (commit)
                    Commit();

                if (entity is IAggregateRoot)
                {
                    var aggregateRoot = entity as IAggregateRoot;
                    foreach (var notification in aggregateRoot.Notifications.Where(x => x is IAfterDeleteDomainEvent<T>))
                        _mediator.Publish(notification);
                }
            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new Msg(ex.Message));
            }
        }

        public virtual void Insert(T entity, bool commit = false)
        {
            try
            {
                if (entity is ISelfValidation<T>)
                {
                    var validation = entity as ISelfValidation<T>;
                    if (!validation.IsValid(_msg))
                        return;
                }

                DbSet.Add(entity);
                if (commit)
                    Commit();

                if(entity is IAggregateRoot)
                {
                    var aggregateRoot = entity as IAggregateRoot;
                    foreach (var notification in aggregateRoot.Notifications.Where(x => x is IAfterInsertDomainEvent<T>))
                        _mediator.Publish(notification);
                }

            }
            catch (Exception ex)
            {
                _msg.Errors.Add(new Msg(ex.Message));
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Queryble();
        }

        public virtual IQueryable<T> QueryBy(Expression<Func<T, bool>> filtro = null)
        {
            return filtro != null ? Queryble().Where(filtro) : Queryble();
        }

        public virtual IQueryable<T> QueryById(long id)
        {
            return Queryble().Where( x => x.Id == id);
        }

        public virtual T GetById(long id)
        {
            return Queryble().FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}

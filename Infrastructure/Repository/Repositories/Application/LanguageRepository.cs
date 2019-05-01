using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.LanguageAggregate;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data.Repository.Repositories.Application
{
    public class LanguageRepository : BaseRepositorySqlServer<LanguageDomain>, IRepository<LanguageDomain>
    {
        public LanguageRepository(DbContextSqlServer context, IMediator mediator, INotificationManager msg) : base(context, mediator, msg)
        {
        }

        public override IQueryable<LanguageDomain> Queryble()
        {
            return DbSet.OrderBy(x => x.Id)
                        .Include(x => x.DataTypes)
                        .ThenInclude( x => x.DataType);
        }
    }
}

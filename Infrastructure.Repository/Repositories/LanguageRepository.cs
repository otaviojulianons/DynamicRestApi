using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.LanguageAggregate;
using Infrastructure.Repositories;
using Infrastructure.Repository.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Messaging;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class LanguageRepository : Repository<LanguageDomain>, IRepository<LanguageDomain>
    {
        public LanguageRepository(AppDbContext context, IMediator mediator, IMsgManager msg) : base(context, mediator, msg)
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

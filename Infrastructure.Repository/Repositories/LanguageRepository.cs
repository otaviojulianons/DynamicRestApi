using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Structure;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class LanguageRepository : Repository<LanguageDomain>, IRepository<LanguageDomain>
    {
        public LanguageRepository(AppDbContext context) : base(context)
        {
        }

        public override IQueryable<LanguageDomain> Queryble()
        {
            return DbSet.OrderBy(x => x.Id)
                        .Include(x => x.DataTypes);
        }
    }
}

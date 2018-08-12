using Domain;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;

namespace Services
{
    public class EntityService
    {
        private ContextRepository<EntityDomain> _entityRepository;

        public EntityService(
            ContextRepository<EntityDomain> entityRepository
            )
        {
            _entityRepository = entityRepository;
        }

        public IEnumerable<EntityDomain> GetAllEntities()
        {
            return _entityRepository.QueryBy()
                    .Include(x => x.Attributes)
                    .ThenInclude(x => x.DataType);
        }
    }
}

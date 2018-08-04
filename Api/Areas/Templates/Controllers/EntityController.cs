using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{

    public class EntityController : RepositoryController<EntityDomain>
    {
        public EntityController(DynamicRepository<EntityDomain> repository) : base(repository)
        {
        }
    }
}
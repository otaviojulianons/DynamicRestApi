using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{

    public class EntityController : DomainController<EntityDomain>
    {
        public EntityController(ContextRepository<EntityDomain> repository) : base(repository)
        {
        }
    }
}
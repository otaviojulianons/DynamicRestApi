using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{

    public class AttributeController : DomainController<AttributeDomain>
    {
        public AttributeController(ContextRepository<AttributeDomain> repository) : base(repository)
        {
        }
    }
}
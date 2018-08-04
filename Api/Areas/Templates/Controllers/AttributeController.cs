using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{

    public class AttributeController : RepositoryController<AttributeDomain>
    {
        public AttributeController(DynamicRepository<AttributeDomain> repository) : base(repository)
        {
        }
    }
}
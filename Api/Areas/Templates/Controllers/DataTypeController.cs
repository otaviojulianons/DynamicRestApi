using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{

    public class DataTypeController : DomainController<DataTypeDomain>
    {
        public DataTypeController(ContextRepository<DataTypeDomain> repository) : base(repository)
        {
        }
    }
}
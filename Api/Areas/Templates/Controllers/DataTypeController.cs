using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{

    public class DataTypeController : RepositoryController<DataTypeDomain>
    {
        public DataTypeController(DynamicRepository<DataTypeDomain> repository) : base(repository)
        {
        }
    }
}
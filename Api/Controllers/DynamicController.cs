using Microsoft.AspNetCore.Mvc;
using Repository;
using SharedKernel.Aplicacao.Api;
using SharedKernel.Repository;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class DynamicController<T> : BaseController where T : class, IEntity
    {
        private DynamicRepository<T> _repository;

        public DynamicController(
            DynamicRepository<T> repository
            )
        {
            _repository = repository;
        }

        [HttpGet()]
        public ResultApi<IEnumerable<T>> List()
        {
            return FormatResult(_repository.GetAll());
        }

        [HttpPost()]
        public ResultApi<bool> Post([FromBody]dynamic item)
        {
            _repository.Insert((T)item);
            return FormatResult(true);
        }

        [HttpGet("{id}")]
        public ResultApi<T> Get(long id)
        {
            return FormatResult(_repository.GetById(id));
        }

        [HttpPut("{id}")]
        public ResultApi<bool> Put(long id,[FromBody]dynamic item)
        {
            _repository.Update(id,(T)item);
            return FormatResult(true);
        }

        [HttpDelete("{id}")]
        public ResultApi<bool> Delete(long id)
        {
            _repository.Delete(id);
            return FormatResult(true);
        }

    }
}

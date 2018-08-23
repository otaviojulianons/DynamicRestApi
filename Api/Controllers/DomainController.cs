using Microsoft.AspNetCore.Mvc;
using Repository;
using SharedKernel.Aplicacao.Api;
using SharedKernel.Repository;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class DomainController<T> : BaseController where T : class, IEntity
    {
        private ContextRepository<T> _repository;

        public DomainController(
            ContextRepository<T> repository
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
        public ResultApi<bool> Post([FromBody]T item)
        {
            _repository.Insert(item);
            return FormatResult(true);
        }

        [HttpGet("{id}")]
        public ResultApi<T> Get(long id)
        {
            return FormatResult(_repository.GetById(id));
        }

        [HttpPut("{id}")]
        public ResultApi<bool> Put(long id,T item)
        {
            _repository.Update(id,item);
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

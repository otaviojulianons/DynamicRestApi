using Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedKernel.Messaging;
using System;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings( IgnoreApi = true)]
    public class DynamicEntityController : BaseController
    {

        public DynamicEntityController(IMsgManager msgs) : base(msgs)
        {
        }

        private dynamic GetRepository()
        {
            Type dynamicType = (Type) HttpContext.Items["DynamicType"];
            var repositoryType = typeof(DynamicRepository<>).MakeGenericType(dynamicType);
            return HttpContext.RequestServices.GetService(repositoryType);
        }

        private dynamic GetEntityModel(dynamic json)
        {
            Type dynamicType = (Type) HttpContext.Items["DynamicType"];
            return JsonConvert.DeserializeObject(json.ToString(), dynamicType);
        }

        [HttpGet()]
        public dynamic List()
        {
            try
            {
                var repository = GetRepository();
                var entities = repository.GetAll();
                return FormatResult(entities);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }   
        }

        [HttpPost()]
        public dynamic Post([FromBody]dynamic json)
        {
            try
            {
                var repository = GetRepository();
                var entity = GetEntityModel(json);
                repository.Insert(entity, true);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public dynamic Get(long id)
        {
            try
            {
                var repository = GetRepository();
                var entity = repository.GetById(id);
                return FormatResult(entity);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }            
        }

        [HttpPut("{id}")]
        public dynamic Put(long id, [FromBody]dynamic json)
        {
            try
            {
                var repository = GetRepository();
                var entity = GetEntityModel(json);
                entity.Id = id;
                repository.Update(entity, true);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public dynamic Delete(long id)
        {
            try
            {
                var repository = GetRepository();
                repository.Delete(id, true);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }
    }
}

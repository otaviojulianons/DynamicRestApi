using Domain.Interfaces.Infrastructure;
using Infrastructure.CrossCutting.Notifications;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings( IgnoreApi = true)]
    public class DynamicEntityController : BaseController
    {

        public DynamicEntityController(INotificationManager msgs) : base(msgs)
        {
        }

        private dynamic GetRepository()
        {
            Type dynamicType = (Type) HttpContext.Items["DynamicType"];
            var repositoryType = typeof(IDynamicRepository<>).MakeGenericType(dynamicType);
            return HttpContext.RequestServices.GetService(repositoryType);
        }

        private dynamic GetEntityModel(dynamic json)
        {
            Type dynamicType = (Type) HttpContext.Items["DynamicType"];
            var result = JsonConvert.DeserializeObject(json.ToString(), dynamicType);

            if (result != null)
                result.Id = Guid.Empty;

            return result;
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
                repository.Insert(entity);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public dynamic Get(Guid id)
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
        public dynamic Put(Guid id, [FromBody]dynamic json)
        {
            try
            {
                var repository = GetRepository();
                var entity = GetEntityModel(json);
                entity.Id = id;
                repository.Update(entity);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public dynamic Delete(Guid id)
        {
            try
            {
                var repository = GetRepository();
                repository.Delete(id);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }
    }
}

using Common.Models;
using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Infrastructure.Dynamic
{
    [Route("[controller]")]
    [ApiExplorerSettings( IgnoreApi = true)]
    public class DynamicEntityController : Controller
    {
        private INotificationManager _msgs;

        public DynamicEntityController(INotificationManager msgs)
        {
            _msgs = msgs;
        }

        private dynamic GetRepository()
        {
            Type dynamicType = (Type) HttpContext.Items["DynamicType"];
            var repositoryType = typeof(IRepository<>).MakeGenericType(dynamicType);
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

        internal ResultDto<T> FormatResult<T>(T result)
        {
            if (_msgs.HasError)
                return FormatError<T>(_msgs.Errors.FirstOrDefault().Message);

            return new ResultDto<T>()
            {
                Result = result,
                Message = "Method executed successfully."
            };
        }

        internal ResultDto<T> FormatError<T>(string message)
        {
            Response.StatusCode = 400;
            return new ResultDto<T>()
            {
                Result = default(T),
                Message = message
            };
        }
    }
}

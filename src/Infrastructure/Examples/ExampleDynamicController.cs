using Common.Models;
using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Examples
{

    [Route("[controller]")]
    [Produces("application/json")]
#if RELEASE
    [ApiExplorerSettings(IgnoreApi = true)]
#endif
    public class ExampleDynamicController : Controller
    {

        protected INotificationManager _msgs;
        protected IGenericRepository<ExampleEntity, Guid> _genericRepository;

        public ExampleDynamicController(
            INotificationManager msgs,
            IGenericRepository<ExampleEntity, Guid> genericRepository)
        {
            _msgs = msgs;
            _genericRepository = genericRepository;
        }

        protected virtual ExampleEntity MapperToDomain(Guid id, ExampleModel model)
        {
            var entity = new ExampleEntity();
            entity.Id = id;
            entity.Name = model.Name;
            return entity;
        }

        [HttpGet("/teste1")]
        public ResultDto<bool> Teste1()
        {
            return FormatResult(true);
        }

        [HttpGet("/teste2")]
        public ResultDto<bool> Teste2(string where)
        {
            var entities = _genericRepository.Queryble().Where(where);
            return FormatResult(true);
        }

        [HttpGet()]
        public ResultDto<IEnumerable<ExampleEntity>> List(string where, string order = "Id", int limit = 1000, int offset = 0)
        {
            IEnumerable<ExampleEntity> entities;
            try
            {
                var query = string.IsNullOrEmpty(where) ?
                    _genericRepository.Queryble() :
                    _genericRepository.Queryble().Where(where);

                entities = query.OrderBy(order)
                                .Skip(offset * limit)
                                .Take(limit);
                return FormatResult(entities);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<ExampleEntity>>(ex.Message);
            }
        }

        [HttpPost()]
        public ResultDto<bool> Post([FromBody]ExampleModel model)
        {
            try
            {
                var entity = MapperToDomain(Guid.NewGuid() ,model);
                _genericRepository.Insert(entity);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ResultDto<ExampleEntity> Get([FromRoute]Guid id)
        {
            try
            {
                var entity = _genericRepository.GetById(id);
                return FormatResult(entity);
            }
            catch (Exception ex)
            {
                return FormatError<ExampleEntity>(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ResultDto<bool> Put([FromRoute]Guid id, [FromBody]ExampleModel model)
        {
            try
            {
                ExampleEntity entity = MapperToDomain(id, model);
                _genericRepository.Update(entity);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ResultDto<bool> Delete(Guid id)
        {
            try
            {
                _genericRepository.Delete(id);
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

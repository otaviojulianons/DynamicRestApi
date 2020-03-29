using Common.Models;
using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Dynamic
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TestDynamicController : Controller
    {
        protected INotificationManager _msgs;
        protected IGenericRepository<TestEntity, Guid> _genericRepository;

        public TestDynamicController(
            INotificationManager msgs,
            IGenericRepository<TestEntity, Guid> genericRepository)
        {
            _msgs = msgs;
            _genericRepository = genericRepository;
        }

        protected virtual TestEntity MapperToDomain(Guid id, TestModel model)
        {
            var entity = new TestEntity();
            entity.Id = id;
            entity.Name = model.Name;
            return entity;
        }

        [HttpGet()]
        public ResultDto<IEnumerable<TestEntity>> List()
        {
            try
            {
                var entities = _genericRepository.GetAll();
                return FormatResult(entities);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<TestEntity>>(ex.Message);
            }
        }

        [HttpPost()]
        public ResultDto<bool> Post([FromBody]TestModel model)
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
        public ResultDto<TestEntity> Get([FromRoute]Guid id)
        {
            try
            {
                var entity = _genericRepository.GetById(id);
                return FormatResult(entity);
            }
            catch (Exception ex)
            {
                return FormatError<TestEntity>(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ResultDto<bool> Put([FromRoute]Guid id, [FromBody]TestModel model)
        {
            try
            {
                TestEntity entity = MapperToDomain(id, model);
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

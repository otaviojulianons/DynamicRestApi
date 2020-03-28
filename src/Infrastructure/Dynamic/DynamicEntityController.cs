using Common.Models;
using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Dynamic
{
    [Route("[controller]")]
    public class DynamicEntityController<TEntity, TId, TModel> : Controller 
        where TEntity : IDynamicEntity<TId,TModel>, new()
        where TId : struct
    {
        private INotificationManager _msgs;
        private IGenericRepository<TEntity, TId> _genericRepository;

        public DynamicEntityController(
            INotificationManager msgs,
            IGenericRepository<TEntity, TId> genericRepository)
        {
            _msgs = msgs;
            _genericRepository = genericRepository;
        }

        [HttpGet()]
        public ResultDto<IEnumerable<TEntity>> List()
        {
            try
            {
                var entities = _genericRepository.GetAll();
                return FormatResult(entities);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<TEntity>>(ex.Message);
            }   
        }

        [HttpPost()]
        public ResultDto<bool> Post([FromBody]TEntity entity)
        {
            try
            {
                if (Equals(entity.Id, Guid.Empty))
                    entity.Id = (TId)Convert.ChangeType(Guid.NewGuid(), typeof(TId));
                _genericRepository.Insert(entity);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ResultDto<TEntity> Get([FromRoute] TId id)
        {
            try
            {
                var entity = _genericRepository.GetById(id);
                return FormatResult(entity);
            }
            catch (Exception ex)
            {
                return FormatError<TEntity>(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public dynamic Put([FromRoute]TId id, [FromBody]TModel model)
        {
            try
            {
                TEntity entity = new TEntity();
                entity.Map(id, model);
                _genericRepository.Update(entity);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public dynamic Delete(TId id)
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

using Api.Filters;
using Application.Commands;
using Application.Models;
using AutoMapper;
using Common.Models;
using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    [Produces("application/json")]
    public class DynamicEntityController : Controller
    {
        private IRepository<EntityDomain> _entityRepository;
        private IMediator _mediator;
        private INotificationManager _msgs;

        public DynamicEntityController(
            IMediator mediator,
            IRepository<EntityDomain> entityRepository,
            INotificationManager msgs
           )
        {
            _entityRepository = entityRepository;
            _mediator = mediator;
            _msgs = msgs;
        }

        [AllowAccess]
        [HttpPost()]
        public async Task<ResultDto<bool>> Post([FromBody]CreateEntityCommand item)
        {
            try
            {
                var result = await _mediator.Send(item);
                return FormatResult(result);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet()]
        public ResultDto<IEnumerable<Entity>> List()
        {
            try
            {
                var entities = _entityRepository.GetAll();
                var models = Mapper.Map<IEnumerable<Entity>>(entities);
                return FormatResult(models);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<Entity>>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ResultDto<Entity> Get(Guid id)
        {
            try
            {
                var entity = _entityRepository.GetById(id);
                var models = Mapper.Map<Entity>(entity);
                return FormatResult(models);
            }
            catch (Exception ex)
            {
                return FormatError<Entity>(ex.Message);
            }
        }              

        [AllowAccess]
        [HttpDelete("{id}")]
        public ResultDto<bool> Delete(Guid id)
        {
            try
            {
                var result = _mediator.Send(new DeleteEntityCommand(id)).Result;
                return FormatResult(result);
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
using Api.Filters;
using Api.Models;
using Application.Models;
using Application.Services;
using AutoMapper;
using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MediatR;
using Application.Commands;
using System.Linq;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EntityController : BaseController
    {
        private IRepository<EntityDomain> _entityRepository;
        private IMediator _mediator;

        public EntityController(
            IMediator mediator,
            IRepository<EntityDomain> entityRepository,
            INotificationManager msgs
           ) : base(msgs)
        {
            _entityRepository = entityRepository;
            _mediator = mediator;
        }

        [AllowAccess]
        [HttpPost()]
        public ResultApi<bool> Post([FromBody]CreateEntityCommand item)
        {
            try
            {
                var result = _mediator.Send(item).Result;
                return FormatResult(result);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet()]
        public ResultApi<IEnumerable<Entity>> List()
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
        public ResultApi<Entity> Get(Guid id)
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
        public ResultApi<bool> Delete(Guid id)
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

    }
}
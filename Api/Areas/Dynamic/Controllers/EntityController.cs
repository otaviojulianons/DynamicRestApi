using Api.Filters;
using Api.Models;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Messaging;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    public class EntityController : BaseController
    {
        private EntityAppService _serviceApp;
        private IRepository<EntityDomain> _entityRepository;

        public EntityController(
            EntityAppService serviceApp,
            IRepository<EntityDomain> entityRepository,
            IMsgManager msgs
           ) : base(msgs)
        {
            _serviceApp = serviceApp;
            _entityRepository = entityRepository;
        }

        [AllowAccess]
        [HttpPost()]
        public ResultApi<bool> Post([FromBody]Entity item)
        {
            try
            {
                _serviceApp.Insert(item);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [AllowAccess]
        [HttpDelete("{id}")]
        public ResultApi<bool> Delete(long id)
        {
            try
            {
                _serviceApp.Delete(id);
                return FormatResult(true);
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
    }
}
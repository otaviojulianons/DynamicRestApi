using Api.Models;
using AutoMapper;
using Domain;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    public class EntityController : BaseController
    {
        private EntityService _service;

        public EntityController(EntityService service)
        {
            _service = service;
        }

        [HttpPost()]
        public ResultApi<bool> Post([FromBody]Entity item)
        {
            try
            {
                var entityDomain = Mapper.Map<EntityDomain>(item);
                _service.Insert(entityDomain);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ResultApi<bool> Delete(long id)
        {
            try
            {
                _service.Delete(id);
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
            var entities = _service.GetAllEntities();
            var entitiesModel = Mapper.Map<IEnumerable<Entity>>(entities);
            return FormatResult(entitiesModel);
        }
    }
}
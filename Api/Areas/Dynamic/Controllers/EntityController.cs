using Api.Models;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using SharedKernel.Aplicacao.Api;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("[controller]")]
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

        [HttpGet()]
        public ResultApi<IEnumerable<Entity>> List()
        {
            var entities = _service.GetAllEntities();
            var entitiesModel = Mapper.Map<IEnumerable<Entity>>(entities);
            return FormatResult(entitiesModel);
        }
    }
}
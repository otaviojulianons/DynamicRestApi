using Api.Models;
using Application.Models;
using Application.Services;
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

        public EntityController(
            EntityAppService serviceApp,
            IMsgManager msgs
           ) : base(msgs)
        {
            _serviceApp = serviceApp;
        }

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
                var entities = _serviceApp.GetAll();
                return FormatResult(entities);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<Entity>>(ex.Message);
            }

        }
    }
}
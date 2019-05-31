using Api.Models;
using Application.Models;
using AutoMapper;
using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    public class DataTypeController : BaseController
    {

        public DataTypeController(
            INotificationManager msgs
        ) : base(msgs)
        {
        }

        [HttpGet()]
        public ResultApi<IEnumerable<DataType>> List()
        {
            try
            {
                var models = Mapper.Map<IEnumerable<DataType>>(null);
                return FormatResult(models);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<DataType>>(ex.Message);
            }
        }
    }
}
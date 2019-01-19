using Api.Models;
using Application.Models;
using AutoMapper;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Messaging;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    public class DataTypeController : BaseController
    {
        private IRepository<DataTypeDomain> _dataTypesRepository;

        public DataTypeController(
            IRepository<DataTypeDomain> dataTypesRepository,
            IMsgManager msgs
        ) : base(msgs)
        {
            _dataTypesRepository = dataTypesRepository;
        }

        [HttpGet()]
        public ResultApi<IEnumerable<DataType>> List()
        {
            try
            {
                var list = _dataTypesRepository.GetAll();
                var models = Mapper.Map<IEnumerable<DataType>>(list);
                return FormatResult(models);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<DataType>>(ex.Message);
            }
        }
    }
}
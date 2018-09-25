using Api.Models;
using AutoMapper;
using Domain.Services;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    public class DataTypeController : BaseController
    {
        private DataTypeService _service;

        public DataTypeController(DataTypeService service)
        {
            _service = service;
        }

        [HttpGet()]
        public ResultApi<IEnumerable<DataType>> List()
        {
            try
            {
                var list =_service.GetAll();
                var models = Mapper.Map<IEnumerable<DataType>>(list);
                return FormatResult(models);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<DataType>>(ex.Message);
            }
        }


        [HttpPost()]
        public ResultApi<bool> Post([FromBody]DataType item)
        {
            try
            {
                var domain = Mapper.Map<DataTypeDomain>(item);
                _service.Insert(domain);
                return FormatResult(true);
            }
            catch (Exception ex)
            {
                return FormatError<bool>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ResultApi<DataType> Get(long id)
        {
            try
            {
                var domain = _service.GetById(id);
                var model = Mapper.Map<DataType>(domain);
                return FormatResult(model);
            }
            catch (Exception ex)
            {
                return FormatError<DataType>(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ResultApi<bool> Put(long id, DataType item)
        {
            try
            {
                var domain = Mapper.Map<DataTypeDomain>(item);
                _service.Update(id,domain);
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

    }
}
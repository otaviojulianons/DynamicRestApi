﻿using Api.Filters;
using Api.Models;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Messaging;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/Dynamic/[controller]")]
    public class DataTypeController : BaseController
    {
        private DataTypeAppService _serviceApp;

        public DataTypeController(
            DataTypeAppService service,
            IMsgManager msgs
        ) : base(msgs)
        {
            _serviceApp = service;
        }

        [HttpGet()]
        public ResultApi<IEnumerable<DataType>> List()
        {
            try
            {
                var list =_serviceApp.GetAll();
                var models = Mapper.Map<IEnumerable<DataType>>(list);
                return FormatResult(models);
            }
            catch (Exception ex)
            {
                return FormatError<IEnumerable<DataType>>(ex.Message);
            }
        }

        [AllowAccess]
        [HttpPost()]
        public ResultApi<bool> Post([FromBody]DataType item)
        {
            try
            {
                var domain = Mapper.Map<DataTypeDomain>(item);
                _serviceApp.Insert(domain);
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
                var domain = _serviceApp.GetById(id);
                var model = Mapper.Map<DataType>(domain);
                return FormatResult(model);
            }
            catch (Exception ex)
            {
                return FormatError<DataType>(ex.Message);
            }
        }

        [AllowAccess]
        [HttpPut("{id}")]
        public ResultApi<bool> Put(long id, DataType item)
        {
            try
            {
                var domain = Mapper.Map<DataTypeDomain>(item);
                _serviceApp.Update(domain);
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

    }
}
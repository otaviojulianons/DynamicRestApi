using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.Filters
{
    public class AllowAccess : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var apiKey = context.HttpContext.Request.Headers["apiKey"].ToString();
            if (!apiKey.Equals(configuration.GetValue<string>("Api:Key")))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new ResultApi<bool>() { Message = "Invalid API key." });
            }

        }
    }
}

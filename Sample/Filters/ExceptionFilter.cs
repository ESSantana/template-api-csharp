using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample.Core.Exceptions;
using Sample.Core.Resources;
using System.Data.Common;
using System.Net;

namespace Sample.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IResourceLocalizer _resource;

        public ExceptionFilter(IResourceLocalizer resource)
        {
            _resource = resource;
        }

        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var result = new { context.Exception.Message };

            if (context.Exception.GetType() == typeof(DbException))
            {
                status = HttpStatusCode.InternalServerError;
                result = new { context.Exception.Message };
            }

            if (context.Exception.GetType() == typeof(CustomException))
            {
                status = HttpStatusCode.BadRequest;
                result = new { context.Exception.Message };
            }

            var response = context.HttpContext.Response;

            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            context.Result = new JsonResult(result);
        }
    }
}

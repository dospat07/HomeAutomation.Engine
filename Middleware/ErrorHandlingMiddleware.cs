using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
               
                await _next(context);
              

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"The following error happened: {e.Message}");
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is CustomException) code = HttpStatusCode.BadRequest;

            // Using RFC 7807 response for error formatting
            // https://tools.ietf.org/html/rfc7807
            var problem = new ProblemDetails
            {
               // Type = "https://yourdomain.com/errors/internal-server-error",
                Title = "Internal Server Error",
                Detail = exception.Message,
                Instance = "",
                Status = (int)code
            };

            var result = JsonConvert.SerializeObject(
                problem,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}

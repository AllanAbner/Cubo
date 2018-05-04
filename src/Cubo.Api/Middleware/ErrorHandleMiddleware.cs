using Cubo.Core.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cubo.Api.Middleware
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
              
                await _next(context);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static Task HandleErrorAsync(HttpContext context, Exception ex)
        {
            var exType = ex.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            var errorCode = "Error";

            switch (ex)
            {
                case CuboException e when exType == typeof(CuboException):
                    errorCode = e.ErrorCode;
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case Exception e when exType == typeof(UnauthorizedAccessException):
                    errorCode = "Unauthorized";
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case Exception e when exType == typeof(ArgumentException):
                    errorCode = "invalid_parameter";
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                default:
                    break;
            }

            var response = new { Message = ex.Message, errorCode = errorCode };

            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}

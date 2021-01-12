using BookStore.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Infrastructure
{
    internal sealed class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static ILogger<ErrorHandlingMiddleware> _logger;
        private static IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            System.Exception exception)
        {
            _logger.LogError(new EventId(exception.HResult),
                exception,
                exception.Message);

            exception = exception.GetBaseException();

            var statusCode = (int)HttpStatusCode.InternalServerError;

            var response = new JsonErrorResponse
            {
                Message = "An internal server error has occured."
            };

            if (exception is RestException re)
            {
                statusCode = (int)re.Code;

                if (re.Message != null)
                {
                    response.Message = re.Message;
                }
            }

            if (_env.IsDevelopment())
            {
                response.DeveloperMessage = $"Exception: {exception.Message} StackTrace: {exception.StackTrace}";
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private class JsonErrorResponse
        {
            public string Message { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}

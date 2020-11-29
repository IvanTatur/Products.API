using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Products.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private static readonly ILogger logger = Log.ForContext<RequestResponseLoggingMiddleware>();
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                logger.Error("The Error is occurred: {0}", exception.Message);
                throw;
            }
            finally
            {
                logger.Information(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
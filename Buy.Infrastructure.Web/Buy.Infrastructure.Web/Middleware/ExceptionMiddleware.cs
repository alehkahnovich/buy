using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Buy.Infrastructure.Web.Handlers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Buy.Infrastructure.Web.Middleware
{
    public sealed class ExceptionMiddleware {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _handler;
        public ExceptionMiddleware(RequestDelegate next, IExceptionHandler handler) {
            _next = next;
            _handler = handler;
        }

        public async Task Invoke(HttpContext context) {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var result = await _handler.GetExceptionPolicyResult(exception).ConfigureAwait(false);
                context.Response.StatusCode = result.StatusCode;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }   
    }
}
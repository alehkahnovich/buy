using System;
using System.Threading.Tasks;
using Buy.Infrastructure.Web.Representations;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Buy.Infrastructure.Web.Handlers
{
    public sealed class ExceptionHandler : IExceptionHandler {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _environment;

        public ExceptionHandler(ILogger logger, IHostingEnvironment environment) {
            _logger = logger;
            _environment = environment;
        }

        public Task<ExceptionPolicyResult> GetExceptionPolicyResult(Exception exception) {
            var code = ExceptionHttpCodeTranslator.Translate(exception);

            var request = Guid.NewGuid().ToString("N");

            var stack = $"{exception.Message}\n{exception.StackTrace}";

            var message = !_environment.IsDevelopment()
                ? $"Please contact support for activity id ${request}"
                : stack;

            _logger.Error($"activity id: [{request}]\n{stack}");

            return Task.FromResult(new ExceptionPolicyResult {
                StatusCode = (int)code,
                ErrorCode = request,
                Message = message
            });
        }
    }
}
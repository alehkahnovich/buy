using System;
using System.Threading.Tasks;
using Buy.Infrastructure.Web.Representations;

namespace Buy.Infrastructure.Web.Handlers
{
    public interface IExceptionHandler {
        Task<ExceptionPolicyResult> GetExceptionPolicyResult(Exception exception);
    }
}
namespace Buy.Infrastructure.Web.Representations
{
    public sealed class ExceptionPolicyResult {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
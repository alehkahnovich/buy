using System;
using System.Collections.Generic;
using System.Net;

namespace Buy.Infrastructure.Web.Handlers
{
    public static class ExceptionHttpCodeTranslator {
        private static readonly IDictionary<string, HttpStatusCode> Codes = new Dictionary<string, HttpStatusCode> {
            { typeof(ArgumentException).FullName, HttpStatusCode.BadRequest },
            { typeof(ArgumentNullException).FullName, HttpStatusCode.BadRequest },
            { typeof(ArgumentOutOfRangeException).FullName, HttpStatusCode.BadRequest }
        };

        public static HttpStatusCode Translate(Exception exception) {
            var type = exception.GetType().FullName;
            if (type == null) return HttpStatusCode.InternalServerError;
            return Codes.ContainsKey(type) ? Codes[type] : HttpStatusCode.InternalServerError;
        }
    }
}
using System;
using Newtonsoft.Json;

namespace Buy.Upload.Contracts.Uploads
{
    public sealed class UploadedRequest {
        [JsonProperty("request_id")]
        public int RequestId { get; set; }
        [JsonProperty("request_key")]
        public Guid UploadKey { get; set; }
        [JsonProperty("name")]
        public string RawName { get; set; }
    }
}
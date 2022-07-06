using System;

namespace Buy.Rasterization.DataAccess.Domain
{
    public class RasterizationRequest {
        public int RequestId { get; set; }
        public Guid UploadKey { get; set; }
        public string RawName { get; set; }
    }
}
using System;

namespace Buy.Upload.Processor.DataAccess.Domain
{
    public sealed class UploadRequest {
        public Guid UploadRequestId { get; set; }
        public string FileName { get; set; }
    }
}
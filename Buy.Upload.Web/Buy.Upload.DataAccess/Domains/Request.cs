using System;

namespace Buy.Upload.DataAccess.Domains
{
    public class Request {
        public int RequestId { get; set; }
        public Guid UploadKey { get; set; }
        public string RawName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
using System.IO;

namespace Buy.Upload.Business.Contracts
{
    public class UploadRequest {
        public UploadRequest(Stream stream, string contentType) {
            Stream = stream;
            ContentType = contentType;
        }

        public string ContentType { get; }
        public Stream Stream { get; }
        public string Key { get; set; }
        public string FileName { get; set; }
    }
}
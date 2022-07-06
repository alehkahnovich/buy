using Buy.Upload.Contracts.Uploads;
using Buy.Upload.DataAccess.Domains;

namespace Buy.Upload.Business.Converters
{
    public static class UploadRequestConvert {
        public static UploadedRequest Convert(Request request) => 
            new UploadedRequest {
                RequestId = request.RequestId,
                RawName = request.RawName,
                UploadKey = request.UploadKey
            };
    }
}
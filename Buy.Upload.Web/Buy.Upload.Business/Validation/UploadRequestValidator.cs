using System;
using System.IO;
using System.Threading.Tasks;
using Buy.Upload.Business.Contracts;
using Buy.Upload.Business.Validation.Abstractions;

namespace Buy.Upload.Business.Validation
{
    public sealed class UploadRequestValidator : IUploadRequestValidator {
        public Task<bool> ValidateAsync(UploadRequest request) {
            if (!IsContentTypeValid(request.ContentType) || !IsExtensionValid(request.FileName))
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        private static bool IsContentTypeValid(string contentType) {
            if (!string.Equals(contentType, "image/jpg", StringComparison.OrdinalIgnoreCase) 
                && !string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) 
                && !string.Equals(contentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) 
                && !string.Equals(contentType, "image/gif", StringComparison.OrdinalIgnoreCase) 
                && !string.Equals(contentType, "image/x-png", StringComparison.OrdinalIgnoreCase) 
                && !string.Equals(contentType, "image/png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(contentType, "image/bmp", StringComparison.OrdinalIgnoreCase))
                return false;
            return true;
        }

        private static bool IsExtensionValid(string fileName) {
            var extension = Path.GetExtension(fileName);
            if (!string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(extension, ".bmp", StringComparison.OrdinalIgnoreCase))
                return false;
            
            return true;
        }
    }
}
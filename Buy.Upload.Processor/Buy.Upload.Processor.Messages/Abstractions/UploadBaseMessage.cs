using System;

namespace Buy.Upload.Processor.Messages.Abstractions
{
    public abstract class UploadBaseMessage {
        public Guid UploadKey { get; set; }
    }
}
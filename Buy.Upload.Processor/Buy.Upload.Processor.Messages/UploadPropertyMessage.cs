using Buy.Upload.Processor.Messages.Abstractions;

namespace Buy.Upload.Processor.Messages {
    public class UploadPropertyMessage : UploadBaseMessage {
        public int CategoryId { get; set; }
    }
}

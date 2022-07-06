using Buy.Upload.Processor.Business.Representation.SourceData.Abstractions;

namespace Buy.Upload.Processor.Business.Representation.SourceData
{
    public class SourceCategory : ISourceData {
        public string Name { get; set; }
        public string ParentName { get; set; }
    }
}
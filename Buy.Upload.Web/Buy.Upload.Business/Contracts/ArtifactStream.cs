using System.IO;

namespace Buy.Upload.Business.Contracts
{
    public sealed class ArtifactStream : Artifact {
        public string ContentType { get; set; }
        public Stream Stream { get; set; }
    }
}
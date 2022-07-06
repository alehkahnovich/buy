using System.Threading.Tasks;
using Buy.Upload.Business.Contracts;

namespace Buy.Upload.Business.Services.Abstractions
{
    public interface IArtifactService {
        Task<ArtifactStream> GetStreamAsync(string type, int requestId);
        Task<ArtifactStream> GetArtifactAsync(int key);
    }
}
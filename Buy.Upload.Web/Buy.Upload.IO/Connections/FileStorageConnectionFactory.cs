using Microsoft.Extensions.Configuration;

namespace Buy.Upload.IO.Connections
{
    internal sealed class FileStorageConnectionFactory : IFileStorageConnectionFactory {
        private const string ConfigurationKey = "BlobStorageConnectionString";
        private readonly IConfiguration _configuration;

        public FileStorageConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string GetConnection() => _configuration.GetSection(ConfigurationKey).Value;
    }
}
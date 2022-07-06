using System.Reflection;
using System.Threading;

namespace Buy.Kafka.Infrastructure.Builders.Abstractions { 
    public interface IKafkaHost {
        void Listen(CancellationToken token, params Assembly[] assemblies);
    }
}

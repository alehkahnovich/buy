using System;
using System.Threading;

namespace Buy.Upload.Processor
{
    internal class Program {
        private static void Main() {
            var source = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                source.Cancel();
            };

            new Kafka.Infrastructure.Builders.KafkaBuilder()
                .ConfigureLogger()
                .ConfigureSettings()
                .ConfigureDependencies()
                .Build()
                .Listen(source.Token, typeof(Program).Assembly);

            Console.WriteLine("Buy.Upload.Processor started");
            Console.ReadKey();
        }
    }
}

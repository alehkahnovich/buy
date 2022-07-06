using System;
using System.Reflection;
using System.Threading;

namespace Buy.Kafka.Debug {
    class Program {
        static void Main(string[] args) {
            var source = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                source.Cancel();
            };

            new Infrastructure.Builders.KafkaBuilder()
                .ConfigureLogger()
                .ConfigureSettings()
                .Build()
                .Listen(source.Token, new Assembly[] { typeof(Program).Assembly });

            Console.ReadKey();
        }
    }
}

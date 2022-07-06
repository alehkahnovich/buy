using System;
using System.Threading;
using System.Threading.Tasks;

namespace Buy.Rasterization.Processor
{
    internal class Program {
        static void Main(string[] args) {
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

            Console.WriteLine("Buy.Rasterization.Processor started");

            Wait(source.Token);
        }

        static void Wait(CancellationToken token) {
            try
            {
                Task.Run(async () => {
                    while (true)
                        await Task.Delay(Timeout.InfiniteTimeSpan, token).ConfigureAwait(false);
                }, token).Wait(token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Buy.Rasterization.Processor stopped");
            }
        }
    }
}

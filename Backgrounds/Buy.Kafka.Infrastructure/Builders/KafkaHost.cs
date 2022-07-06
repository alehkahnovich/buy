using Buy.Kafka.Consumers;
using Buy.Kafka.Consumers.Serialization;
using Buy.Kafka.Consumers.Attributes;
using Buy.Kafka.Infrastructure.Builders.Abstractions;
using Buy.Kafka.Infrastructure.Settings;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Buy.Kafka.Infrastructure.Builders
{
    internal sealed class KafkaHost : IKafkaHost {
        private const int MessageArity = 1;
        private static readonly Type Consumer = typeof(IConsumerProcessor<,>);
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _container;
        public KafkaHost(IServiceCollection container, IConfiguration configuration, ILogger logger) {
            _container = container;
            _configuration = configuration;
            _logger = logger;
        }

        private ConsumerConfig ResolveSettings() {
            var consumer = _configuration.GetSection("Consumer").Get<ConsumerSettings>();

            return new ConsumerConfig {
                BootstrapServers = consumer.BootstrapServers,
                GroupId = consumer.GroupId,
                EnableAutoCommit = consumer.EnableAutoCommit,
                SessionTimeoutMs = consumer.SessionTimeoutMs
            };
        }

        public void Listen(CancellationToken token, params Assembly[] assemblies) {
            var configuration = ResolveSettings();
            var consumers = assemblies.SelectMany(assembly => assembly.GetTypes().Where(src => GetConsumerDescriptor(src) != null)).ToList();
            foreach (var consumer in consumers) {
                _container.AddScoped(Consumer.MakeGenericType(GetConsumerDescriptor(consumer).GetGenericArguments()), consumer);
            }

            var provider = _container.BuildServiceProvider();

            foreach (var consumer in consumers) {
                ThreadPool.QueueUserWorkItem((state) => {
                    var type = (Type) state;
                    var descriptor = GetConsumerDescriptor(type);
                    var arguments = descriptor.GetGenericArguments();
                    var kafka = typeof(ConsumerBuilder<,>).MakeGenericType(arguments);
                    var topic = type.GetCustomAttribute<TopicAttribute>();
                    if (topic == null) return;

                    var instance = Activator.CreateInstance(kafka, new object[] { configuration });
                    var build = instance.GetType().GetMethod(nameof(ConsumerBuilder<string, string>.Build));

                    SetParsers(instance, arguments);

                    using (var subscriber = build?.Invoke(instance, new object[] { }) as IDisposable) {
                        Subscribe(subscriber, new[] { topic.Topic });
                        while (!token.IsCancellationRequested) {
                            var result = Consume(subscriber, token);

                            using (var scope = provider.CreateScope()) {
                                var processor = scope.ServiceProvider.GetRequiredService(Consumer.MakeGenericType(arguments));

                                try
                                {
                                    var job = (Task)processor.GetType().GetMethod(nameof(IConsumerProcessor<string, string>.ConsumeAsync))
                                        ?.Invoke(processor, new[] { result });

                                    job?.Wait(token);

                                    Commit(subscriber, result);
                                }
                                catch (OperationCanceledException) {
                                    _logger.Debug($"Processor cancelled. {processor.GetType()}");
                                    throw;
                                }
                                catch (Exception exception) {
                                    _logger.Error(exception, "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}");
                                }
                            }
                        }
                    }

                }, consumer);
            }
        }

        private static void Subscribe(object subscriber, IEnumerable<string> topics) {
            var subscribe = GetMethod(nameof(IConsumer<string, string>.Subscribe), subscriber.GetType(), typeof(IEnumerable<string>));
            subscribe?.Invoke(subscriber, new object[] { topics });
        }

        private static object Consume(object consumer, CancellationToken token) {
            var consume = GetMethod(nameof(IConsumer<string, string>.Consume), consumer.GetType(), typeof(CancellationToken));
            return consume?.Invoke(consumer, new object[] { token });
        }

        private static object Commit(object consumer, object message) {
            var commit = GetMethod(nameof(IConsumer<string, string>.Commit), consumer.GetType(), typeof(ConsumeResult<,>));
            return commit?.Invoke(consumer, new object[] { message });
        }

        private static void SetParsers(object instance, Type[] arguments) {
            var setParser = instance.GetType().GetMethod(nameof(ConsumerBuilder<string, string>.SetValueDeserializer));
            var parser = Activator.CreateInstance(typeof(ValueDeserializer<>).MakeGenericType(arguments[MessageArity]));
            setParser?.Invoke(instance, new[] { parser });
        }

        private static MethodInfo GetMethod(string name, Type subscriber, Type argument) {
            const int parametersQuantity = 1;
            var method = name;
            var methods = subscriber
                .GetMethods()
                .Where(src => string.Equals(src.Name, method, StringComparison.Ordinal));

            foreach (var entry in methods) {
                var parameters = entry.GetParameters().ToList();
                if (parameters.Count != parametersQuantity) continue;
                if (argument.IsGenericType && GetAssignableDescriptor(parameters.Single().ParameterType, argument) != null)
                    return entry;
                if (!argument.IsAssignableFrom(parameters.Single().ParameterType)) continue;
                return entry;
            }

            throw new MethodAccessException();
        }

        private static Type GetConsumerDescriptor(Type type) => GetAssignableDescriptor(type, Consumer);

        private static Type GetAssignableDescriptor(Type type, Type generic) {
            while (true) {
                var definition = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (definition != null && definition == generic)
                    return definition;

                foreach (var @interface in type.GetInterfaces()) {
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == generic)
                        return @interface;
                }

                var @base = type.BaseType;

                if (@base == null) return null;

                type = @base;
            }
        }
    }
}

using System;
using Buy.Infrastructure.Library.Dependencies;
using Buy.Upload.Processor.Business.Merge;
using Buy.Upload.Processor.Business.Merge.Abstractions;
using Buy.Upload.Processor.Business.Parsers;
using Buy.Upload.Processor.Business.Parsers.Abstractions;
using Buy.Upload.Processor.Business.Parsers.Types;
using Buy.Upload.Processor.Business.Processors;
using Buy.Upload.Processor.Business.Processors.Abstractions;
using Buy.Upload.Processor.DataAccess.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Upload.Processor.Business
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.BootDependencies(typeof(IO.Bootstrap));
            container.BootDependencies(typeof(DataAccess.Bootstrap));

            container.AddScoped<CsvParser>();
            container.AddScoped<TxtParser>();
            container.AddSingleton<Func<ParserType, IParser>>(provider => parser => ResolveParser(provider, parser));
            container.AddSingleton<IParsingFactory, ParsingFactory>();
            container.AddScoped<ICategoryProcessor, CategoryProcessor>();
            container.AddScoped<IBulkMerge<RawCategory>, CategoryBulkMerge>();
        }

        private static IParser ResolveParser(IServiceProvider container, ParserType type) {
            switch (type) {
                case ParserType.Csv:
                    return container.GetRequiredService<CsvParser>();
                case ParserType.Txt:
                    return container.GetRequiredService<TxtParser>();
                default:
                    return null;
            }
        }
    }
}
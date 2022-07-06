using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Buy.Upload.Processor.Business.Representation.SourceData.Abstractions;
using CsvHelper;
using CsvHelper.Configuration;
using IParser = Buy.Upload.Processor.Business.Parsers.Abstractions.IParser;

namespace Buy.Upload.Processor.Business.Parsers.Base
{
    public abstract class CsvBaseParser : IParser {
        protected abstract string Delimiter { get; }
        public async Task<IEnumerable<TRaw>> Parse<TRaw>(Stream stream) where TRaw : ISourceData {
            var results = new List<TRaw>();
            using (var reader = new StreamReader(stream)) {
                using (var csv = new CsvReader(reader, new Configuration { Delimiter = Delimiter })) {
                    while (await csv.ReadAsync().ConfigureAwait(false)) {
                        var record = csv.GetRecord<TRaw>();
                        results.Add(record);
                    }
                }
            }
            return results;
        }
    }
}
using Buy.Upload.Processor.Business.Parsers.Base;

namespace Buy.Upload.Processor.Business.Parsers
{
    public sealed class CsvParser : CsvBaseParser {
        protected override string Delimiter => ",";
    }
}
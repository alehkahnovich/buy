using Buy.Upload.Processor.Business.Parsers.Base;

namespace Buy.Upload.Processor.Business.Parsers
{
    public class TxtParser : CsvBaseParser {
        protected override string Delimiter => "\t";
    }
}
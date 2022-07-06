using System.ComponentModel;

namespace Buy.Upload.Processor.Business.Parsers.Types
{
    public enum ParserType {
        [Description(".csv")]
        Csv,
        [Description(".txt")]
        Txt
    }
}
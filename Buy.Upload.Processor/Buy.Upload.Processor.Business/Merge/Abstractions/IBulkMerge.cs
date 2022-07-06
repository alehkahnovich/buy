using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buy.Upload.Processor.Business.Merge.Abstractions
{
    public interface IBulkMerge<in TRaw> {
        Task MergeAsync(IEnumerable<TRaw> parents, IEnumerable<TRaw> siblings);
    }
}
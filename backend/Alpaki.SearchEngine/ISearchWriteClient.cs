using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpaki.SearchEngine
{
    public interface ISearchWriteClient
    {
        Task RebuildSearchData<T>(IEnumerable<T> items, string indexName = null);
    }
}

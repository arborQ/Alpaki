using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Alpaki.SearchEngine.SearchItems
{
    public class CarBrandSearchItem
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string BrandCode { get; set; }

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene, IsSortable = true)]
        public string BrandName { get; set; }
    }
}

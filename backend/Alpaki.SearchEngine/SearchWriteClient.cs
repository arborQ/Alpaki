using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Alpaki.SearchEngine
{
    class SearchWriteClient : ISearchWriteClient
    {
        private readonly SearchIndexClient _searchIndexClient;

        public SearchWriteClient(SearchClientConfiguration searchClientConfiguration)
        {
            _searchIndexClient = new SearchIndexClient(new Uri(searchClientConfiguration.EndPoint), new AzureKeyCredential(searchClientConfiguration.ApiKey));
        }

        public async Task RebuildSearchData<T>(IEnumerable<T> items, string indexName = null)
        {
            var searchClient = await CreateOrUpdateIndexAsync<T>(indexName);

            var result = await searchClient.MergeOrUploadDocumentsAsync(items);
        }

        private async Task<SearchClient> CreateOrUpdateIndexAsync<T>(string indexName = null)
        {
            indexName = string.IsNullOrEmpty(indexName) ? typeof(T).Name : indexName;

            var fieldBuilder = new FieldBuilder();
            var searchFields = fieldBuilder.Build(typeof(T));
            var definition = new SearchIndex(indexName, searchFields);
            await _searchIndexClient.CreateOrUpdateIndexAsync(definition);

            return _searchIndexClient.GetSearchClient(indexName);
        }
    }
}

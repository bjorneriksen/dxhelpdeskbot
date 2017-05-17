using DXHelpDeskBot.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace DXHelpDeskBot.Services
{
    [Serializable]
    public class AzureSearchService
    {
        string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
        string searchIndexName = ConfigurationManager.AppSettings["SearchIndexName"];
        string searchqueryApiKey = ConfigurationManager.AppSettings["SearchQueryApiKey"];

        public async Task<DocumentSearchResult<Models.SearchResult>> SearchAsync(string serviceNameList, string term)
        {
            SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, searchIndexName, new SearchCredentials(searchqueryApiKey));

            SearchParameters parameters;
            DocumentSearchResult<Models.SearchResult> results;

            string[] serviceList = serviceNameList.Split(',');
            string filter = String.Join(" or ", serviceList.Select(i => $"(Service eq '{i}')"));

            parameters =
                new SearchParameters()
                {
                    Select = new[] { "Title", "URL", "Description" }, //return fields
                    OrderBy = new[] { "LastUpdated desc" },
                    Filter = filter,
                    Top = 30
                };
            string ffFilter = "baseRate lt 150";

            results = await indexClient.Documents.SearchAsync<Models.SearchResult>(term, parameters);

            return results;
        }
        
    }
}
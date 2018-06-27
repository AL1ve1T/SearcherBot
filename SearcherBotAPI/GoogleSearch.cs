using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace SearcherBotAPI
{
    public static partial class SearchAPI
    {
        public static List<GoogleSearchResult> GoogleSearch(string query, string apiKey, string engineId)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = engineId;

            IList<Result> results = new List<Result>();
            List<GoogleSearchResult> searchResults = new List<GoogleSearchResult>();

            int counter = 0;

            while (searchResults.Count < 10)
            {
                listRequest.Start = counter * 10 + 1;
                results = listRequest.Execute().Items;

                if (results == null) break;

                foreach (var item in results)
                {
                    GoogleSearchResult result = new GoogleSearchResult();
                    result.Title = item.Title;
                    result.Description = item.Snippet;
                    result.Link = item.Link;

                    searchResults.Add(result);
                }
                counter++;
            }
            
            return searchResults;
        }
    }
}

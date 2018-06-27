using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SearcherBotAPI
{
    public static partial class SearchAPI
    {
        public static List<YouTubeSearchResult> YouTubeSearch(string query, string apiKey)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "SearcherBot"
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = query;
            searchRequest.MaxResults = 20;

            var searchResponse = searchRequest.Execute();

            List<YouTubeSearchResult> links = new List<YouTubeSearchResult>();

            foreach (var searchResult in searchResponse.Items)
            {
                YouTubeSearchResult result = new YouTubeSearchResult();
                result.Title = searchResult.Snippet.Title;

                switch(searchResult.Id.Kind)
                {
                    case "youtube#video":
                        result.Link = String.Format("https://www.youtube.com/watch?v={0}", searchResult.Id.VideoId);
                        links.Add(result);
                        break;

                    case "youtube#channel":
                        result.Link = String.Format("https://www.youtube.com/channel/{0}", searchResult.Id.ChannelId);
                        links.Add(result);
                        break;

                    case "youtube#playlist":
                        result.Link = String.Format("https://www.youtube.com/watch?v={0}", searchResult.Id.PlaylistId);
                        links.Add(result);
                        break;
                }
            }
            return links;
        }
    }
}

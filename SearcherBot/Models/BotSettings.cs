using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearcherBot.Models
{
    public static class BotSettings
    {

        public static string Url { get; set; } = "https://searcherbot.azurewebsites.net:443/{0}";

        public static string Name { get; set; } = "aliveit_searcher_bot";

        public static string Key { get; set; } = "603892572:AAFSzGa2rsiDd2ytnivVvfPAqAUubTovtzU";

        public static string GoogleApiKey { get; set; } = "AIzaSyC4r-3OEvr8s6zhRSrYH2jpGMXwk0di5Dw";

        public static string GoogleSearchEngineId { get; set; } = "009158199219010684512:y2b86stbkkw";

        public static string DataSource { get; set; } = "searcherbot.database.windows.net";

        public static string UserID { get; set; } = "AliveIT";

        public static string Password { get; set; } = "E1l2n3u4r5";

        public static string InitialCatalog { get; set; } = "SearcherBotDB";

    }
}
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

        public static string Key { get; set; } = "";

    }
}
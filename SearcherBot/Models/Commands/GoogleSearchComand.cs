using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using SearcherBotAPI;

namespace SearcherBot.Models.Commands
{
    public class GoogleSearchComand : Command
    {
        public override string Name => "/google";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            if (message.Text.Split().Count() == 1)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Please enter the search query");
                return;
            }
            string query = message.Text.Substring(message.Text.IndexOf(" ") + 1);
            List<string> searchResults = SearchAPI.GoogleSearch(query, BotSettings.GoogleApiKey, BotSettings.GoogleSearchEngineId);

            foreach (var link in searchResults)
            {
                await client.SendTextMessageAsync(message.Chat.Id, link);
            }
        }
    }
}
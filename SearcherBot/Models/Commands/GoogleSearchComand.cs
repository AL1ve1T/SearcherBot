using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SearcherBotAPI;

namespace SearcherBot.Models.Commands
{
    public class GoogleSearchComand : Command
    {
        public override string Name => "/google";

        public override void Execute(Message message, TelegramBotClient client)
        {
            if (!IsWaiting())
            {
                client.SendTextMessageAsync(message.Chat.Id, "Using: /google <search_query>");
                return;
            }

            string query = message.Text.Substring(message.Text.IndexOf(" ") + 1);
            List<GoogleSearchResult> searchResults = SearchAPI.GoogleSearch(query, BotSettings.GoogleApiKey, BotSettings.GoogleSearchEngineId);

            foreach (var result in searchResults)
            {
                string msg = "*Title:* " + result.Title + '\n' +
                    "*Description:* " + result.Description + '\n' +
                    "*Link:* " + result.Link;
                client.SendTextMessageAsync(message.Chat.Id, msg, ParseMode.Markdown);
            }
        }

        public override bool IsWaiting()
        {
            throw new NotImplementedException();
        }
    }
}
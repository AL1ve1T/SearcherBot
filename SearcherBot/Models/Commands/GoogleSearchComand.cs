using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SearcherBotAPI;
using Telegram.Bot.Types.ReplyMarkups;

namespace SearcherBot.Models.Commands
{
    public class GoogleSearchComand : Command
    {
        public override string Name => "/google";

        public override void Execute(Message message, TelegramBotClient client)
        {
            List<GoogleSearchResult> searchResults = SearchAPI.GoogleSearch(message.Text, BotSettings.GoogleApiKey, BotSettings.GoogleSearchEngineId);

            foreach (var result in searchResults)
            {
                string msg = "*Title:* " + result.Title + '\n' +
                    "*Description:* " + result.Description + '\n' +
                    "*Link:* " + result.Link;
                client.SendTextMessageAsync(message.Chat.Id, msg, ParseMode.Markdown);
            }
        }
    }
}
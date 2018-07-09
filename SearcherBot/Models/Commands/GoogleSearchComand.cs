using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SearcherBotAPI;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text;

namespace SearcherBot.Models.Commands
{
    public class GoogleSearchComand : Command
    {
        public override string Name => "/google";

        public override void Execute(Message message, TelegramBotClient client)
        {
            List<GoogleSearchResult> searchResults = SearchAPI.GoogleSearch(message.Text, BotSettings.GoogleApiKey, BotSettings.GoogleSearchEngineId);
            string msg = null;

            foreach (var result in searchResults)
            {
                msg = "*Title:* " + result.Title + '\n' +
                    "*Link:* " + result.Link + '\n' +
                    "*Description:* " + result.Description;

                client.SendTextMessageAsync(message.Chat.Id, msg, ParseMode.Markdown);
            }
        }
    }
}
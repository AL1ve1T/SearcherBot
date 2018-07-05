using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using SearcherBotAPI;
using Telegram.Bot.Types.Enums;

namespace SearcherBot.Models.Commands
{
    public class YoutubeSearchCommand : Command
    {
        public override string Name => "/youtube";

        public override void Execute(Message message, TelegramBotClient client)
        {
            if (!IsWaiting())
            {
                client.SendTextMessageAsync(message.Chat.Id, "Using: /youtube <search_query>");
                return;
            }

            string query = message.Text.Substring(message.Text.IndexOf(" ") + 1);
            List<YouTubeSearchResult> searchResults = SearchAPI.YouTubeSearch(query, BotSettings.GoogleApiKey);

            foreach (var result in searchResults)
            {
                string msg = "*Title:* " + result.Title + '\n' +
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
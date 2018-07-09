using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using SearcherBotAPI;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SearcherBot.Models.Commands
{
    public class YoutubeSearchCommand : Command
    {
        public override string Name => "/youtube";

        public override void Execute(Message message, TelegramBotClient client)
        {
            List<YouTubeSearchResult> searchResults = SearchAPI.YouTubeSearch(message.Text, BotSettings.GoogleApiKey);
            string msg = null;

            foreach (var result in searchResults)
            {
                msg = "*Title:* " + result.Title + '\n' +
                    "*Link:* " + result.Link;

                client.SendTextMessageAsync(message.Chat.Id, msg, ParseMode.Markdown);
            }
        }
    }
}
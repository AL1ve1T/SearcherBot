using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;

using Telegram.Bot;
using SearcherBot.Models.Commands;

namespace SearcherBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            //TODO: Add more commands

            client = new TelegramBotClient(BotSettings.Key);
            var hook = string.Format(BotSettings.Url, "api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;

using Telegram.Bot;
using SearcherBot.Models.Commands;
using System.Data.SqlClient;

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
            commandsList.Add(new StartCommand());
            commandsList.Add(new GoogleSearchComand());
            commandsList.Add(new YoutubeSearchCommand());

            client = new TelegramBotClient(BotSettings.Key);
            var hook = string.Format(BotSettings.Url, "api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }

        public static SqlConnection GetDBConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = BotSettings.DataSource;
            builder.UserID = BotSettings.UserID;
            builder.Password = BotSettings.Password;
            builder.InitialCatalog = BotSettings.InitialCatalog;

            return new SqlConnection(builder.ConnectionString);

        }
    }
}
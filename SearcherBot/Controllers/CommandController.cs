using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

using Telegram.Bot.Types;
using SearcherBot.Models;
using System.Web.Http.Results;
using System.Diagnostics;
using Telegram.Bot.Requests;
using SearcherBot.Models.Commands;
using Telegram.Bot;
using System.Data.SqlClient;

namespace SearcherBot.Controllers
{
    public class CommandController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var inlineCommands = Bot.InlineCommands;
            var client = await Bot.Get();

            var message = update.Message;
            var query = update.CallbackQuery;

            if (query != null)
            {
                ExecuteQuery(query, client);
            }
            else
            {
                ExecuteCommand(message, client, commands, inlineCommands);
            }
            return Ok();
        }

        [NonAction]
        private void ExecuteQuery(CallbackQuery query, TelegramBotClient client)
        {
            client.AnswerCallbackQueryAsync(query.Id, "Success!");

            using (SqlConnection connection = Bot.GetDBConnection())
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                sqlCommand.CommandText = $"INSERT INTO Commands (ChatId, Command) VALUES ({query.Message.Chat.Id}, '{query.Data}')";
                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }

            client.SendTextMessageAsync(query.Message.Chat.Id, "Ok! What do you want to search?");
        }

        [NonAction]
        private void ExecuteCommand(Message message, TelegramBotClient client, IReadOnlyList<Command> commands, IReadOnlyList<Command> inlineCommands)
        {
            if (message.Text[0] == '/')
            {
                foreach (var command in inlineCommands)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(message, client);
                        break;
                    }
                }
                return;
            }

            using (SqlConnection connection = Bot.GetDBConnection())
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                sqlCommand.CommandText = $"SELECT DISTINCT Command FROM Commands WHERE ChatId = {message.Chat.Id}";

                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    foreach (var command in commands)
                    {
                        if (command.Contains(reader[0].ToString().Trim(' ')))
                        {
                            command.Execute(message, client);
                            break;
                        }
                    }
                    reader.Close();

                    sqlCommand.CommandText = $"DELETE FROM Commands WHERE ChatId = {message.Chat.Id}";
                    sqlCommand.ExecuteNonQuery();
                }

                if (!reader.IsClosed) reader.Close();
                connection.Close();
            }
        }
    }
}
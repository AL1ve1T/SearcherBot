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
            var client = await Bot.Get();

            var message = update.Message;
            var query = update.CallbackQuery;

            if (query != null)
            {
                await client.AnswerCallbackQueryAsync(query.Id, "Success!");

                using (SqlConnection connection = Bot.GetDBConnection())
                {
                    await connection.OpenAsync();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;

                    sqlCommand.CommandText = $"INSERT INTO Commands (ChatId, Command) VALUES ({query.Message.Chat.Id}, '{query.Data}')";
                    sqlCommand.ExecuteNonQuery();

                    connection.Close();
                }

                await client.SendTextMessageAsync(query.Message.Chat.Id, "Ok! What do you want to search?");
            }
            else
            {
                ExecuteCommand(message, client, commands);
            }
            return Ok();
        }

        [NonAction]
        private async void ExecuteCommand(Message message, TelegramBotClient client, IReadOnlyList<Command> commands)
        {
            using (SqlConnection connection = Bot.GetDBConnection())
            {
                await connection.OpenAsync();
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
                else
                {
                    foreach (var command in commands)
                    {
                        if (command.Contains(message.Text))
                        {
                            command.Execute(message, client);
                            break;
                        }
                    }
                }
                if (!reader.IsClosed) reader.Close();
                connection.Close();
            }
        }
    }
}
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
                ExecuteCommand(query.Message, query.Data, client, commands);
                return Ok();
            }
            
            ExecuteCommand(message, message.Text, client, commands);
            
            return Ok();
        }

        [NonAction]
        private void ExecuteCommand(Message message, string cmd, TelegramBotClient client, IReadOnlyList<Command> commands)
        {
            foreach (var command in commands)
            {
                if (command.Contains(cmd.Split()[0]))
                {
                    command.Execute(message, client);
                    break;
                }
            }
        }
    }
}
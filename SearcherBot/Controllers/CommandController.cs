using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

using Telegram.Bot.Types;
using SearcherBot.Models;
using System.Web.Http.Results;

namespace SearcherBot.Controllers
{
    public class CommandController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }

            return Ok();
        }
    }
}
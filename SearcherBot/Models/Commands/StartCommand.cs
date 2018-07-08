using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SearcherBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    new InlineKeyboardButton{Text="Google", CallbackData="/google"},
                    new InlineKeyboardButton{Text="Youtube", CallbackData="/youtube"}
                }
            });

            client.SendTextMessageAsync(message.Chat.Id, "Where do you want to search?", replyMarkup: keyboard);
        }
    }
}
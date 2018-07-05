﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SearcherBot.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command == this.Name;
        }

        public abstract bool IsWaiting();
    }
}
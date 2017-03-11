using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Voice;
using CommandInterpreter;
using Discord.Models;

namespace Discord
{
    public class Bot
    {
        DiscordClient _client;
        Interpreter _interpreter;

        public Bot(string token, Interpreter interpreter)
        {
            _interpreter = interpreter;
            _client = new DiscordClient(new DiscordConfig()
            {
                Token = token,
                AutoReconnect = true
            });

            _client.Connect();
            _client.MessageCreated += MessageCreated;
        }

        private async void MessageCreated(object sender, MessageCreateEventArgs e)
        {
            var discordEvent = new DiscordEvent(e);
            _interpreter.Interpret(e.Message.Content, discordEvent);
        }
    }
}
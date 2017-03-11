using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Models
{
    public class Channel
    {
        private DiscordChannel _channel;
        public static Channel GetChannel(object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordChannel = e.Channel;

            return new Channel(discordChannel);
        }

        private Channel(DiscordChannel discordChannel)
        {
            _channel = discordChannel;
        }

        public void PrintMessage(string message)
        {
            _channel.SendMessage(message);
        }

    }
}

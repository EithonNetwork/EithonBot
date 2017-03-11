using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Models
{
    public class Guild
    {
        private DiscordGuild _guild;
        public static Guild GetGuild(object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordGuild = e.Guild;

            return new Guild(discordGuild);
        }

        private Guild(DiscordGuild discordGuild)
        {
            _guild = discordGuild;
        }

    }
}

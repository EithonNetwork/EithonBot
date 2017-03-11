using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Models
{
    internal class DiscordEvent
    {
        public DiscordGuild Guild { get; private set; }

        public bool HasUser { get; private set; }
        private DiscordUser _user;
        public DiscordUser User
        {
            get
            {
                if (!HasUser) throw new ApplicationException("Illegal usage.");
                return _user;
            }
            private set
            {
                _user = value;
            }
        }

        public bool HasMessage { get; private set; }
        private DiscordMessage _message;
        public DiscordMessage Message
        {
            get
            {
                if (!HasMessage) throw new ApplicationException("Illegal usage.");
                return _message;
            }
            private set
            {
                _message = value;
            }
        }
        private DiscordChannel _channel;
        public DiscordChannel Channel
        {
            get
            {
                if (!HasMessage) throw new ApplicationException("Illegal usage.");
                return _channel;
            }
            private set
            {
                _channel = value;
            }
        }

        public bool MessageIncludeRoles { get; private set; }
        private List<DiscordRole> _discordRoles;
        public List<DiscordRole> DiscordRoles
        {
            get
            {
                if (!MessageIncludeRoles) throw new ApplicationException("Illegal usage.");
                return _discordRoles;
            }
            private set
            {
                _discordRoles = value;
            }
        }

        public DiscordEvent(MessageCreateEventArgs e)
        {
            Guild = e.Guild;

            HasMessage = true;
            Message = e.Message;

            if (e.MentionedRoles != null)
            {
                MessageIncludeRoles = true;
                DiscordRoles = e.MentionedRoles;
            }
            Channel = e.Channel;
        }

        public DiscordEvent(GuildMemberUpdateEventArgs e)
        {
            Guild = e.Guild;

            HasUser = true;
            User = e.User;
        }
    }
}

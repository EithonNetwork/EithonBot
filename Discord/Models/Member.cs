using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Models
{
    public class Member
    {
        private DiscordMember _member;

        public static Member GetMember(string memberString, object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordMember = e.Guild.GetAllMembers().Result.FirstOrDefault(m => m.User.Username == memberString || m.Nickname == memberString);

            return new Member(discordMember);
        }

        public static Member GetMember(ulong memberId, object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordMember = e.Guild.GetMember(memberId).Result;

            return new Member(discordMember);
        }

        private Member(DiscordMember discordMember)
        {
            _member = discordMember;
        }
    }
}

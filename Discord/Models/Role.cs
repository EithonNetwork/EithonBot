using DSharpPlus;
using System;

namespace Discord.Models
{
    public class Role
    {
        private DiscordRole _role;
        public ulong ID { get { return _role.ID; } }

        public static Role GetRole(string roleString, object context)
        {
            var e = context as DiscordEvent;

            if (e == null) throw new ArgumentException("Context was of unexpected type");
            DiscordRole discordRole;
            foreach (DiscordRole role in e.Guild.Roles)
            {
                if (role.Name == roleString)
                {
                    discordRole = role;
                    return new Role(discordRole);
                }
            }
            throw new ArgumentException("Rolename did not match a role on the server");
        }

        /*public static Role GetRole(object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordMember = e.Guild.GetMember(memberId).Result;
            e.DiscordRoles.

            return new Member(discordMember);
        }*/

        private Role(DiscordRole discordRole)
        {
            _role = discordRole;
        }
    }
}
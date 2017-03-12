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
        private DiscordGuild _guild;
        private DiscordMember _member;
        public ulong Id => _member.User.ID;

        private string _nickname;
        public string Nickname { get { return _nickname; } set { _nickname = value; } }
        //public List<ulong> Roles => _member.Roles;
        private List<ulong> _roles;
        public List<ulong> Roles { get { return _roles; } set { _roles = value; } }

        private bool _isMuted;
        public bool IsMuted { get { return _isMuted; } set { _isMuted = value; } }
        private bool _isDeafened;
        public bool IsDeafened { get { return _isDeafened; } set { _isDeafened = value; } }

        public static Member GetMember(string memberString, object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordMember = e.Guild.GetAllMembers().Result.FirstOrDefault(m => m.User.Username == memberString || m.Nickname == memberString);
            return new Member(discordMember, e.Guild);
        }

        public static Member GetMember(ulong memberId, object context)
        {
            var e = context as DiscordEvent;
            if (e == null) throw new ArgumentException("Context was of unexpected type");
            var discordMember = GetMember(memberId, e.Guild);
            return new Member(discordMember, e.Guild);
        }

        public void Refresh()
        {
            var discordMember = GetMember(Id, _guild);

            CopyFromDiscordMember(discordMember);
        }

        private static DiscordMember GetMember(ulong memberId, DiscordGuild guild)
        {
            return guild.GetMember(memberId).Result;
        }

        private Member(DiscordMember discordMember, DiscordGuild guild)
        {
            _guild = guild;

            CopyFromDiscordMember(discordMember);
        }

        private void CopyFromDiscordMember(DiscordMember discordMember)
        {
            _member = discordMember;
            Nickname = _member.Nickname;
            Roles = _member.Roles;

            IsMuted = _member.IsMuted;
            IsDeafened = _member.IsDeafened;
        }

        public void AssignRole(Role role)
        {
            Roles.Add(role.ID);

            _guild.ModifyMember(Id, Nickname, Roles, IsMuted, IsDeafened, 0);

            Refresh();
        }
    }
}

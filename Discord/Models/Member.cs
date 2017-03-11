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
            var discordMember = e.Guild.GetMember(memberId).Result;

            return new Member(discordMember, e.Guild);
        }

        private Member(DiscordMember discordMember, DiscordGuild guild)
        {
            _guild = guild;

            _member = discordMember;
            Nickname = _member.Nickname;
            Roles = _member.Roles;

            IsMuted = _member.IsMuted;
            IsDeafened = _member.IsDeafened;
        }
        
        public void AssignRole(Role role)
        {
            List<ulong> newRoles = _member.Roles;
            newRoles.Add(role.ID);

            _guild.ModifyMember(_member.User.ID, _member.Nickname, newRoles, _member.IsMuted, _member.IsDeafened, 0);
            Roles = newRoles;
        }
    }
}

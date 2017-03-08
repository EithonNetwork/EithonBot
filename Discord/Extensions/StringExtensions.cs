using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Extensions
{
    public static class StringExtensions
    {
        public static Models.Member AsMember(this string parameterValue, object context)
        {
            var member = Models.Member.GetMember(parameterValue, context);
            return member;
        }

        public static Models.Role AsRole(this string parameterValue, object context)
        {
            var role = Models.Role.GetRole(parameterValue, context);
            return role;
        }
    }
}

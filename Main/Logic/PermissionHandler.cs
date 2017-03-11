using CommandInterpreter;
using CommandInterpreter.Models;
using Discord.Extensions;
using Discord.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Logic
{
    class PermissionHandler
    {
        public PermissionHandler(Interpreter interpreter)
        {
            interpreter
                .AddCommandLine("perm user <user> assign <role>")
                .SetDescription("Assign a role to a user")
                .SetAction(PermissionHandler.AssignRole);

            interpreter
                .AddCommandLine("perm user <user> unassign <role>")
                .SetDescription("Unassign a role from a user")
                .SetAction(PermissionHandler.UnAssignRole); ;

            interpreter
                .AddCommandLine("perm role <role> canassign <role>")
                .SetDescription("Allow a role to assign another role")
                .SetAction(PermissionHandler.CanAssignRole); ;

        }

        internal static void AssignRole(Command command)
        {
            var member = command.GetParameterValue("<user>").AsMember(command.Context);
            var role = command.GetParameterValue("<role>").AsRole(command.Context);


            //var guild = Discord.Models.Guild.GetGuild(command.Context);

            member.AssignRole(role);

            var roles = member.Roles;

            var channel = Discord.Models.Channel.GetChannel(command.Context);
            Console.WriteLine($"User entered the following successful command: {role.ID}");
            channel.PrintMessage("Assigned role to user");
            Console.WriteLine($"User entered the following successful command: {command.UserCommandInput}");



            
        }

        private static void UnAssignRole(Command command)
        {
            throw new NotImplementedException();
        }

        private static void CanAssignRole(Command command)
        {
            throw new NotImplementedException();
        }
    }
}

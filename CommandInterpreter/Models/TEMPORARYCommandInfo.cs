﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeyBot.Logic.Models
{
    class CommandInfo
    {
        //TODO: "bool includedInHelp"
        string _name;
        private List<CommandParam> _params = new List<CommandParam>();
        private List<CommandInfo> _subCommands = new List<CommandInfo>();
        string _description;
        Func<CommandEventArgs, Task> _action;

        private CommandInfo(string name)
        {
            _name = name;
        }

        public static CommandInfo CreateCommand(string name)
        {
            return new CommandInfo(name);
        }

        public CommandInfo CreateSubCommand(string name)
        {
            var subCommand = new CommandInfo(name);
            subCommand.Parent = this;
            _subCommands.Add(subCommand);
        }

        public bool IsLast => !_subCommands.Any();

        public CommandInfo Parent { get; private set; }

        public CommandInfo AddParams(string paramName, bool isRequired)
        {
            CommandParam param = new CommandParam(paramName, isRequired);
            _params.Add(param);
            return this;
        }

        public CommandInfo SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public CommandInfo SetAction(Func<CommandEventArgs, Task> action)
        {
            _action = action;
            return this;
        }

        //

        public string GetMainCommand()
        {
            return _name;
        }

        public string GetSubCommand()
        {
            return _subCommand;
        }

        public List<CommandParam> GetParams()
        {
            return _params;
        }

        public string GetDescription()
        {
            return _description;
        }

        public Func<CommandEventArgs, Task> GetAction()
        {
            return _action;
        }
    }
}

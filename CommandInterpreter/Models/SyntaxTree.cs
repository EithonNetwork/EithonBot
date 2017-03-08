using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterpreter.Models
{
    public class SyntaxTree
    {
        public delegate void CommandAction(Command command);

        string _commandName;
        private List<Param> _params = new List<Param>();
        private Dictionary<string, SyntaxTree> _subSyntaxDictionary = new Dictionary<string, SyntaxTree>();
        private CommandAction _commandAction;

        //string _description;
        //Func<CommandEventArgs, Task> _action;
        private bool _isRepeat;
        private SyntaxTree _parent;
        
        public SyntaxTree()
        {
            _commandName = null;
        }

        public string Description { get; private set; }

        public SyntaxTree SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public SyntaxTree SetAction(CommandAction permUserAssign)
        {
            _commandAction = permUserAssign;
            return this;
        }

        private SyntaxTree(string commandName, SyntaxTree parent)
        {
            _commandName = commandName;
            _parent = parent;
        }

        internal CommandSyntax ParseCommandTokens(List<string> tokens)
        {
            var token = GetAndRemoveFirstToken(tokens);
            if (token == null) return new CommandSyntax(this);

            if (!IsVariable(token))
            {
                var subSyntax = AddSubCommand(token);
                return subSyntax.ParseCommandTokens(tokens);
            }
            else
            {
                if(!_isRepeat) AddParameter(token);
                return ParseCommandTokens(tokens);
            }
        }

        internal SyntaxTree AddSubCommand(string token)
        {
            SyntaxTree subSyntax;
            if (!_subSyntaxDictionary.ContainsKey(token))
            {
                subSyntax = new SyntaxTree(token, this);
                _subSyntaxDictionary.Add(token, subSyntax);
            }
            else
            {
                subSyntax = _subSyntaxDictionary[token];
                subSyntax._isRepeat = true;
            }
            return subSyntax;
        }

        internal void AddParameter(string token)
        {
            // TODO: Add required or not to syntax
            var param = new Param(token, true);
            _params.Add(param);
        }

        internal List<string> GetCommands()
        {
            return GetCommands("");
        }

        private List<string> GetCommands(string beginning)
        {
            if (IsCommand())
            {
                if (beginning != "") beginning += " ";
                beginning += _commandName;
            }
            foreach (var param in _params)
            {
                beginning = $"{beginning} {param.Name}";
            }
            var result = new List<string>();
            if (!_subSyntaxDictionary.Any())
            {
                result.Add(beginning);
            }
            else
            {
                foreach (var syntax in _subSyntaxDictionary.Values)
                {
                    var list = syntax.GetCommands(beginning);
                    result.AddRange(list);
                }
            }
            return result;
        }

        private bool IsVariable(string token)
        {
            return (token.StartsWith("<") && token.EndsWith(">"));
        }

        private bool IsCommand()
        {
            return _parent != null;
        }

        private bool IsBaseCommand()
        {
            return IsCommand() && !_parent.IsCommand();
        }

        internal void Interpret(List<string> userCommandTokens, Command command)
        {
            string userCommandToken;
            foreach (var param in _params)
            {
                userCommandToken = GetAndRemoveFirstToken(userCommandTokens);
                if (userCommandToken == null)  throw new ApplicationException($"Missing parameter {param.Name}");
                command.AddParam(param.Name, userCommandToken);
            }

            userCommandToken = GetAndRemoveFirstToken(userCommandTokens);
            if (userCommandToken == null)
            {
                //TODO: Better throw message
                if (_commandAction == null) throw new ApplicationException("Not complete command");
                _commandAction(command);
                return;
            }

            if (_subSyntaxDictionary.ContainsKey(userCommandToken))
            {
                var subSyntax = _subSyntaxDictionary[userCommandToken];
                subSyntax.Interpret(userCommandTokens, command);
                return;
            }

            throw new ApplicationException($"Unexpected token: {userCommandToken}.");
        }

        private string GetAndRemoveFirstToken(List<string> tokens)
        {
            if (!tokens.Any()) return null;
            var token = tokens[0];
            tokens.RemoveAt(0);
            return token;
        }

        /// <summary>
        /// For debug purposes
        /// </summary>
        /// <returns>The command name, or "Root" if command name is empty.</returns>
        public override string ToString()
        {
            return _commandName??"Root";
        }
    }
}

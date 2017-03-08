using CommandInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterpreter
{
    public class Interpreter
    {
        private string _prefix;

        //TODO: Explain what this does
        internal SyntaxTree Syntax { get; private set; }

        public Interpreter(string prefix)
        {
            _prefix = prefix;
            Syntax = new SyntaxTree();
        }

        public void Interpret(string userCommandInput, object context)
        {
            var command = new Command(userCommandInput);
            command.Context = context;
            var userCommandTokens = userCommandInput.Split(' ').ToList();
            try
            {
                Syntax.Interpret(userCommandTokens, command);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public CommandSyntax AddCommandLine(string syntaxAsString)
        {
            var tokens = syntaxAsString.Split(' ').ToList();
            return Syntax.ParseCommandTokens(tokens);
        }

        public List<string> GetCommands()
        {
            return Syntax.GetCommands()
                .Select(c => _prefix + c)
                .ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterpreter.Models
{
    public class CommandSyntax
    {
        private SyntaxTree _leaf;
        internal CommandSyntax(SyntaxTree leaf)
        {
            _leaf = leaf;
        }

        public CommandSyntax SetDescription(string description)
        {
            _leaf.SetDescription(description);
            return this;
        }

        public CommandSyntax SetAction(SyntaxTree.CommandAction action)
        {
            _leaf.SetAction(action);
            return this;
        }
    }
}

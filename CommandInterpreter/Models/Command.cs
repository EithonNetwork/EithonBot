using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterpreter.Models
{
    public class Command
    {
        private Dictionary<string, string> _parameterValues;

        public Command(string userCommandInput)
        {
            UserCommandInput = userCommandInput;
            _parameterValues = new Dictionary<string, string>();
        }

        public object Context { get; internal set; }

        public string UserCommandInput { get; private set; }

        public void AddParam(string parameterName, string parameterValue)
        {
            _parameterValues.Add(parameterName, parameterValue);
        }

        public string GetParameterValue(string parameterName)
        {
            return _parameterValues[parameterName];
        }
        
    }
}

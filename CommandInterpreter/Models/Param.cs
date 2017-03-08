using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterpreter.Models
{
    public class Param
    {

        public Param(string paramName, bool isRequired)
        {
            Name = paramName;
            IsRequired = isRequired;
        }

        public string Name { get; private set; }

        public bool IsRequired { get; private set; }
    }
}

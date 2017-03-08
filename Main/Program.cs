using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using CommandInterpreter;
using CommandInterpreter.Models;
using Main.Logic;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {

            var interpreter = new Interpreter("!");

            //Initiate permission commands
            new PermissionHandler(interpreter);

            //TODO: Replace with help command (Currently displays all commands in console)
            var lines = interpreter.GetCommands();
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            var token = ConfigurationManager.AppSettings["Token"];
            var bot = new Discord.Bot(token, interpreter);
            Console.ReadLine();
        }
    }
}
using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MockRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Test how to", Line = "Test Line", Platform = "Test Platform" },
                new Command { Id = 1, HowTo = "Test 2", Line = "Test Line1", Platform = "Test Platform1" },
                new Command { Id = 2, HowTo = "Test 3", Line = "Test Line2", Platform = "Test Platform2" }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Test how to", Line = "Test Line", Platform = "Test Platform" };
        } 
    }
}

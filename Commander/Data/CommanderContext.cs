using Commander.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        // Representation of our command model in our db with DbSet
        public DbSet<Command> Commands { get; set; }
    }
}

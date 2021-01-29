using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repos = new MockRepo();

        public CommandsController(ICommanderRepo repsitory)
        {
            _repos = repsitory;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repos.GetAllCommands();

            return Ok(commandItems);
        }

        // GET request that will responde to URL api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult <Command> GetCommandById(int id)
        {
            var singleCommandItem = _repos.GetCommandById(id);

            return Ok(singleCommandItem);
        }
    }
}

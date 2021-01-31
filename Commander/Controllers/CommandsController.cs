using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        //private readonly ICommanderRepo _repos = new MockRepo();
        private readonly ICommanderRepo _repos;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repsitory, IMapper mapper)
        {
            _repos = repsitory;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repos.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET request that will responde to URL api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var singleCommandItem = _repos.GetCommandById(id);

            if (singleCommandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(singleCommandItem));
            }
            return NotFound();
        }

        // POST api/commands
        [HttpPost]
        public ActionResult  <CommandReadDto> CreateCommand(CommandCreateDto commCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commCreateDto);
            _repos.CreateCommand(commandModel);
            _repos.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // When run in the context of http, it will return the serialized object in the body, but you should see a header in the response with the link to the resource
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commUpdateDto)
        {
            var commandModelFromRepo = _repos.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commUpdateDto, commandModelFromRepo);

            // Good practice
            _repos.UpdateCommand(commandModelFromRepo);

            _repos.SaveChanges();

            return NoContent(); // 204
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repos.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);

            patchDoc.ApplyTo(commToPatch, ModelState);

            if (TryValidateModel(commToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commToPatch, commandModelFromRepo);
           
            _repos.UpdateCommand(commandModelFromRepo);

            _repos.SaveChanges();

            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repos.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repos.DeleteCommand(commandModelFromRepo);

            _repos.SaveChanges();

            return NoContent();
        }
    }
}

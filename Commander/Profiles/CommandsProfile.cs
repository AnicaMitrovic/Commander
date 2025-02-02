﻿using AutoMapper;
using AutoMapper.Configuration;
using Commander.Dtos;
using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source (Command) --> target/destination (ReadDto)
            CreateMap<Command, CommandReadDto>();

            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandUpdateDto, Command>();

            CreateMap<Command, CommandUpdateDto>();
        }
    }
}

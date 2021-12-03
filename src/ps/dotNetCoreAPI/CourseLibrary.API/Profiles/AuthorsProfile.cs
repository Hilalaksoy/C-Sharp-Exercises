﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CourseLibrary.API.Profiles
{
    public class AuthorsProfile: Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorDto>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest =>dest.Age,
                    opt => opt.MapFrom(src =>  src.DateOfBirth.GetCurrentAge()));

            CreateMap<CreateAuthorDto, Author>();
        }
        
    }
}

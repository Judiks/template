using AutoMapper;
using System;
using System.Collections.Generic;
using THD.Domain.Entities;
using THD.Domain.Models.AccountModels.Request;
using THD.Domain.Models.AccountModels.Response;

namespace THD.Infrastructure.Helpers.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Request's
            CreateMap<CreateUserRequest, ApplicationUser>();
           
            CreateMap<ApplicationUser, UserResponse>();
           
            // Response's
          
            CreateMap<ApplicationUser, UserResponse>();
            
            // Dto's
           
            //Entities
           

        }
    }
}

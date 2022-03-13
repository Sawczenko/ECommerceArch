using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using IdentityServer.Dtos;

namespace IdentityServer.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}

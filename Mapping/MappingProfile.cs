
using AutoMapper;
using DataAcces.Model;
using Domain.DTO;
using System.Security.Principal;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

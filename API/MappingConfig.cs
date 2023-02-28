using API.Models.Dto;
using API.Models;
using AutoMapper;

namespace API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}

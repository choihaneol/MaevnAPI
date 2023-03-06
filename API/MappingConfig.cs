using API.Models;
using API.Models.Dto;
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

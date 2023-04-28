using AutoMapper;
using Postr.DTO;
using Postr.Models;

namespace Postr.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer() 
        { 
            CreateMap<SignupDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}

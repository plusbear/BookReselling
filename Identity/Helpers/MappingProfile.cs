using AutoMapper;
using Identity.DataTransferObjects;
using Identity.Models;

namespace Identity.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}

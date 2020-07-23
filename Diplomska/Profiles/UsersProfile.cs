using System.Linq;
using AutoMapper;
using Diplomska.DTOS.UsersDTO;
using Diplomska.Entities;

namespace Diplomska.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>().ForMember(
                dest => dest.Name,
                opt =>
                    opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserToUpdateDto, User>();
            CreateMap<User, UserToUpdateDto > ();
        }
    }
}
using AutoMapper;
using Diplomska.DTOS.ExperienceDTO;
using Diplomska.Entities;

namespace Diplomska.Profiles
{
    public class ExperienceProfile : Profile
    {
        public ExperienceProfile()
        {
            CreateMap<Experience, ExperienceDto>();
            CreateMap<ExperienceForCreation, Experience>();
            CreateMap<ExperienceToUpdate, Experience>();
            CreateMap<Experience, ExperienceToUpdate>();
        }
    }
}
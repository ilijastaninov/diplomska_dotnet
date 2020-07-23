using AutoMapper;
using Diplomska.DTOS.EducationDTO;
using Diplomska.Entities;

namespace Diplomska.Profiles
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {
            CreateMap<Education, EducationDto>();
            CreateMap<EducationForCreationDto, Education>();
            CreateMap<EducationToUpdateDto, Education>();
            CreateMap<Education, EducationToUpdateDto>();
        }
    }
}
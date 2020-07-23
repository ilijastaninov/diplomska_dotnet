using AutoMapper;
using Diplomska.DTOS.CoursesDTO;
using Diplomska.Entities;

namespace Diplomska.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<CourseForUpdateDto, Course>();
        }
    }
}
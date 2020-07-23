using System;
using System.Collections.Generic;
using Diplomska.DTOS.UsersDTO;

namespace Diplomska.DTOS.CoursesDTO
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        //public List<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
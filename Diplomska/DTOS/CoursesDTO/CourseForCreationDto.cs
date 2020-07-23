using System.ComponentModel.DataAnnotations;

namespace Diplomska.DTOS.CoursesDTO
{
    public class CourseForCreationDto
    {
        [Required(ErrorMessage = "Course name is required.")]
        [MaxLength(100,ErrorMessage = "Maximum length is 100 characters.")]
        public string CourseName { get; set; }
    }
}
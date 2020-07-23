using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diplomska.Entities
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required(ErrorMessage ="Please enter the course name")]
        [MaxLength(100,ErrorMessage = "The provided course name is to long. Maximum number of characters is 100.")]
        public string CourseName { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}
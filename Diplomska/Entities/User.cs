using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomska.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Status { get; set; }
        [MaxLength(1500)]
        public string Bio { get; set; }

        public ICollection<Education> Educations{ get; set; }
            = new List<Education>();
        public ICollection<Experience> Experiences { get; set; }
            = new List<Experience>();
        public ICollection<Post> Posts{ get; set; }
            = new List<Post>();
        
        /*public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();*/

        public ICollection<UserCourse> UserCourses{ get; set; } = new List<UserCourse>();
    }
}
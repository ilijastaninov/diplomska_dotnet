using System;
using System.ComponentModel.DataAnnotations;

namespace Diplomska.Entities
{
    public class Experience
    {
        [Key]
        public Guid ExperienceId { get; set; }
        [Required(ErrorMessage = "Title field is required.")]
        [MaxLength(100,ErrorMessage = "The maximum amount is 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Company field is required.")]
        [MaxLength(100, ErrorMessage = "The maximum amount is 100 characters")]
        public string Company { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Current { get; set; } = false;
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
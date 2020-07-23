using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomska.Entities
{
    public class Education
    {
        [Key]
        public Guid EducationId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Degree { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
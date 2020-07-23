using System;

namespace Diplomska.DTOS.ExperienceDTO
{
    public class ExperienceDto
    {
        public Guid ExperienceId { get; set; }
        public string Title { get; set; }
        
        public string Company { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Current { get; set; } = false;
        public Guid UserId { get; set; }
    }
}
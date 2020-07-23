using System;

namespace Diplomska.DTOS.EducationDTO
{
    public class EducationDto
    {
        public Guid EducationId { get; set; }
        
        public string Degree { get; set; }
        
        public DateTime? From { get; set; }
        
        public DateTime? To { get; set; }
        public Guid UserId { get; set; }
    }
}
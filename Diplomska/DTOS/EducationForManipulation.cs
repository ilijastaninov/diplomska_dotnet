using System;
using System.ComponentModel.DataAnnotations;
using Diplomska.ValidationAttributes;

namespace Diplomska.DTOS
{
    [FromToValidation(ErrorMessage = "The TO date cannot be less than the FROM date")]
    
    public abstract class EducationForManipulation
    {
        [Required(ErrorMessage = "You should fill out a degree")]
        [MaxLength(200, ErrorMessage = "The degree shouldn't have more than 200 characters.")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "You should enter a from-date")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Please enter a valid From date")]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "You should enter a to-date")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Please enter a valid To date")]
        public DateTime To { get; set; }
    }
}
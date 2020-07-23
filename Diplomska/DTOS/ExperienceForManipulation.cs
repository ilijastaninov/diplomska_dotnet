using System;
using System.ComponentModel.DataAnnotations;
using Diplomska.ValidationAttributes;

namespace Diplomska.DTOS
{
    [FromToValidationExp(ErrorMessage = "The TO date cannot be less than the FROM date")]
    public abstract class ExperienceForManipulation
    {
        [Required(ErrorMessage = "Title field is required.")]
        [MaxLength(100, ErrorMessage = "The maximum amount is 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Company field is required.")]
        [MaxLength(100, ErrorMessage = "The maximum amount is 100 characters")]
        public string Company { get; set; }
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Please enter a valid To date")]
        public DateTime From { get; set; }
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Please enter a valid To date")]
        public DateTime To { get; set; }
        public bool Current { get; set; }
    }
}
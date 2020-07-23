using System.ComponentModel.DataAnnotations;
using Diplomska.DTOS;
using Diplomska.DTOS.EducationDTO;

namespace Diplomska.ValidationAttributes
{
    public class FromToValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (EducationForManipulation) validationContext.ObjectInstance;
            var result = course.To.CompareTo(course.From);
            if (result < 0)
            {
                 return
                    new ValidationResult(ErrorMessage,
                        new[] { "FromToValidation" }
                    );
            }
            return ValidationResult.Success;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using Diplomska.DTOS;

namespace Diplomska.ValidationAttributes
{
    public class FromToValidationExp : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (ExperienceForManipulation)validationContext.ObjectInstance;
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
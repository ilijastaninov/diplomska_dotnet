using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Diplomska.ValidationAttributes;

namespace Diplomska.DTOS.EducationDTO
{
    
    public class EducationForCreationDto : EducationForManipulation//: IValidatableObject 
    {

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = To.CompareTo(From);

            if (result < 0)
            {
                yield return 
                    new ValidationResult("The TO attribute cannot be less than or equal to the FROM attribute.",
                        new [] { "EducationForCreationDto" }
                        );
            }
        }*/
    }
}
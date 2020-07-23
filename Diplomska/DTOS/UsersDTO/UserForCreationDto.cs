using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Diplomska.DTOS.EducationDTO;

namespace Diplomska.DTOS.UsersDTO
{
    public class UserForCreationDto : UserForManipulation
    {
        /*[Required(ErrorMessage = "Username is required")]
        [MaxLength(30,ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(30, ErrorMessage = "Password cannot be longer than 30 characters.")]
        public string Password { get; set; }
        [MaxLength(50,ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [MaxLength(1500,ErrorMessage = "Max length is 1500 characters long.")]
        public string Bio { get; set; }
        public ICollection<EducationForCreationDto> Educations { get; set; }
        = new List<EducationForCreationDto>();*/
    }
}
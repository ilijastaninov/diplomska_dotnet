using System;
using System.ComponentModel.DataAnnotations;

namespace Diplomska.jwt.models
{
    public class JwtRequest
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

    }
}
using System;
using System.Collections.Generic;
using Diplomska.DTOS.CoursesDTO;

namespace Diplomska.DTOS.UsersDTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string Status { get; set; }
        
        public string Bio { get; set; }
        
        
    }
}
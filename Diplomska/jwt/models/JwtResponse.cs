using System;
using Diplomska.Entities;

namespace Diplomska.jwt.models
{
    public class JwtResponse : User
    {
        public Guid Id { get; set; }
        public string Username{ get; set; }
        public string Password{ get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public JwtResponse(User user,string token)
        {
            Token = token;
            Username = user.Username;
            Password = user.Password;
            Status = user.Status;
            Email = user.Email;

        }
    }
}
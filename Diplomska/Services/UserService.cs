using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Diplomska.Context;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Diplomska.jwt;
using Diplomska.jwt.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Diplomska.Services
{
    public class UserService : IUserInterface
    {
        private readonly ConnectorDbContext context;
        private readonly AppSettings _appSettings;

        public UserService(ConnectorDbContext _context, IOptions<AppSettings> appSettings)
        {
            context = _context;
            _appSettings = appSettings.Value;
        }
        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList<User>();
        }

        public User GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users.Include(u=>u.UserCourses).ThenInclude(uc=>uc.Course)
                .Where(u=>u.Id==userId).FirstOrDefault(u => u.Id == userId);
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Id = Guid.NewGuid();
            foreach (var education in user.Educations)
            {
                education.EducationId = Guid.NewGuid();
            }

            context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            context.Users.Remove(user);
        }

        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users.Any(u => u.Id == userId);
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public bool UsernameExists(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            return context.Users.Any(u => u.Username == username);
        }

        public JwtResponse Authenticate(JwtRequest model)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var token = generateJwtToken(user);
            return new JwtResponse(user,token);
        }
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
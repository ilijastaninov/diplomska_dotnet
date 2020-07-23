using System;
using System.Collections.Generic;
using Diplomska.Entities;
using Diplomska.jwt.models;

namespace Diplomska.Interfaces
{
    public interface IUserInterface
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool UserExists(Guid userId);
        bool UsernameExists(string username);
        bool Save();
        JwtResponse Authenticate(JwtRequest model);

    }
}
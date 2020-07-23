using System;
using System.Collections.Generic;
using Diplomska.Entities;

namespace Diplomska.Interfaces
{
    public interface IEducationInterface
    {
        IEnumerable<Education> GetEducations(Guid userId);
        Education GetEducation(Guid userId, Guid educationId);
        void AddEducation(Guid userId, Education education);
        void DeleteEducation(Education education);
        void UpdateEducation(Education education);
        bool UserExists(Guid userId);
        User GetUser(Guid id);
        bool Save();
    }
}
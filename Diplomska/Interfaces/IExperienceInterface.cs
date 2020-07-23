using System;
using System.Collections.Generic;
using Diplomska.Entities;

namespace Diplomska.Interfaces
{
    public interface IExperienceInterface
    {
        IEnumerable<Experience> GetAllExperiencesForUser(Guid userId);
        Experience GetExperienceById(Guid userId, Guid experienceId);
        void AddExperience(Guid userId, Experience experience);
        void UpdateExperience(Experience experience);
        void DeleteExperience(Experience experience);
        bool Save();
        bool UserExists(Guid userId);
        User GetUser(Guid id);
    }
}
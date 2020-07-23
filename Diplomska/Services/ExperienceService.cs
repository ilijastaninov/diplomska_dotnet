using System;
using System.Collections.Generic;
using System.Linq;
using Diplomska.Context;
using Diplomska.Entities;
using Diplomska.Interfaces;

namespace Diplomska.Services
{
    public class ExperienceService : IExperienceInterface
    {
        private readonly ConnectorDbContext context;

        public ExperienceService(ConnectorDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<Experience> GetAllExperiencesForUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Experiences.Where(e => e.UserId == userId).ToList();
        }

        public Experience GetExperienceById(Guid userId, Guid experienceId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (experienceId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(experienceId));
            }

            return context.Experiences
                .Where(e => e.UserId == userId && e.ExperienceId == experienceId)
                .FirstOrDefault();
        }

        public void AddExperience(Guid userId, Experience experience)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }

            experience.UserId = userId;
            context.Experiences.Add(experience);
        }

        public void UpdateExperience(Experience experience)
        {
            
        }

        public void DeleteExperience(Experience experience)
        {
            context.Experiences.Remove(experience);
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users.Any(a => a.Id == userId);
        }
        public User GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users
                .FirstOrDefault(u => u.Id == userId);
        }
    }
}
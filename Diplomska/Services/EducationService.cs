using Diplomska.Entities;
using Diplomska.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Diplomska.Context;

namespace Diplomska.Services
{
    public class EducationService : IEducationInterface
    {
        private readonly ConnectorDbContext context;
        public EducationService(ConnectorDbContext _context)
        {
            context = _context;
        }
        public void AddEducation(Guid userId, Education education)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (education == null)
            {
                throw new ArgumentNullException(nameof(education));
            }

            education.UserId = userId;
            context.Educations.Add(education);
        }

        public void DeleteEducation(Education education)
        {
            context.Educations.Remove(education);
        }

        public Education GetEducation(Guid userId, Guid educationId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (educationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(educationId));
            }

            return context.Educations
                .Where(e => e.UserId == userId && e.EducationId == educationId).FirstOrDefault();
        }

        public IEnumerable<Education> GetEducations(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Educations.Where(e => e.UserId == userId).ToList();
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public void UpdateEducation(Education education)
        {
            
        }

        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users.Any(a => a.Id== userId);
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
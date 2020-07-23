using System;
using System.Linq;
using Diplomska.Context;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplomska.Services
{
    public class UserCourseService : IUserCourseInterface
    {
        private readonly ConnectorDbContext _context;

        public UserCourseService(ConnectorDbContext context)
        {
            _context = context;
        }
        public UserCourse AddUserCourse(UserCourse userCourse)
        {
            User user = _context.Users
                .FirstOrDefault(u => u.Id == userCourse.UserId);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Course course = _context.Courses.FirstOrDefault(c => c.CourseId == userCourse.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            UserCourse uscr = new UserCourse()
            {
                Course = course,
                User = user
            };
            _context.UserCourses.Add(uscr);
            _context.SaveChanges();
            return uscr;
        }
    }
}
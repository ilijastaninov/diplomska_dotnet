using Diplomska.Entities;
using Diplomska.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Diplomska.Context;

namespace Diplomska.Services
{
    public class CourseService : ICourseInterface
    {
        private readonly ConnectorDbContext context;

        public CourseService(ConnectorDbContext _context)
        {
            context = _context;
        }
        public void AddCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            course.CourseId = Guid.NewGuid();
            context.Courses.Add(course);
        }

        public bool CourseExists(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return context.Courses.Any(c => c.CourseId == courseId);
        }

        public bool CourseNameExists(string courseName)
        {
            if (courseName == null)
            {
                throw new ArgumentNullException(nameof(courseName));
            }

            return context.Courses.Any(c => c.CourseName == courseName);
        }

        public void DeleteCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            context.Courses.Remove(course);
        }

        public Course GetCourse(Guid id)

        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return context.Courses.FirstOrDefault(c => c.CourseId == id);
        }

        public IEnumerable<Course> GetCourses()
        {
            return context.Courses.ToList();
        }

        public bool Save()
        {
            return context.SaveChanges() >= 0;
        }

        public void UpdateCourse(Course course)
        {
            
        }
    }
}
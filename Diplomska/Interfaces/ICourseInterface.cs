using System;
using System.Collections.Generic;
using Diplomska.Entities;

namespace Diplomska.Interfaces
{
    public interface ICourseInterface
    {
        IEnumerable<Course> GetCourses();
        Course GetCourse(Guid id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        bool CourseExists(Guid courseId);
        bool CourseNameExists(string courseName);
        bool Save();
    }
}
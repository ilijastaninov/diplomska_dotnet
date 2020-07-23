using System;
using System.Collections.Generic;
using AutoMapper;
using Diplomska.DTOS.CoursesDTO;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Diplomska.Controllers
{
    [Authorize]
    [EnableCors("CorsAPI")]
    [ApiController]
    [Route("/api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICourseInterface repository;

        public CourseController(IMapper _mapper, ICourseInterface _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetAllCourses()
        {
            var courses = repository.GetCourses();
            return Ok(mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}",Name = "GetCourse")]
        public ActionResult<CourseDto> GetCourseById(Guid courseId)
        {
            var course = repository.GetCourse(courseId);
            if (course == null)
            {
                return BadRequest(new JsonResult("Course doesn't exist."));
            }

            return Ok(mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public ActionResult<CourseDto> PostCourse(CourseForCreationDto course)
        {
            var courseEntity = mapper.Map<Course>(course);
            if (repository.CourseNameExists(courseEntity.CourseName))
            {
                return BadRequest(new JsonResult("Course already exists."));
            }
            repository.AddCourse(courseEntity);
            repository.Save();
            var courseToReturn = mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourse", new {courseId = courseToReturn.CourseId}, courseToReturn);
        }

        [HttpPut("{courseId}")]
        public ActionResult<CourseDto> UpdateCourse(Guid courseId,CourseForUpdateDto courseUpdate)
        {
            var course = repository.GetCourse(courseId);
            if (course == null)
            {
                return NotFound(new JsonResult($"The course with the id: {course.CourseId} doesn't exist."));
            }

            var courseEntity = mapper.Map(courseUpdate, course);
            repository.UpdateCourse(courseEntity);
            repository.Save();
            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public ActionResult<CourseDto> DeleteCourseById(Guid courseId)
        {
            var course = repository.GetCourse(courseId);
            if (course == null)
            {
                return NotFound(new JsonResult($"The course with the id: {course.CourseId} doesn't exist."));
            }
            repository.DeleteCourse(course);
            repository.Save();
            return NoContent();
        }
    }
}
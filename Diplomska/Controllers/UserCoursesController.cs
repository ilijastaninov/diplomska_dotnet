using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diplomska.Context;
using Diplomska.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;

namespace Diplomska.Controllers
{
    [EnableCors("CorsAPI")]
    [Authorize]
    [Route("api/usercourse")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly ConnectorDbContext _context;

        public UserCoursesController(ConnectorDbContext context)
        {
            _context = context;
        }

        // GET: api/UserCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCourse>>> GetUserCourses()
        {
            return await _context.UserCourses.ToListAsync();
        }

        // GET: api/UserCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCourse>> GetUserCourse(Guid id)
        {
            var userCourse = await _context.UserCourses.FindAsync(id);

            if (userCourse == null)
            {
                return NotFound();
            }

            return userCourse;
        }

        // PUT: api/UserCourses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCourse(Guid id, UserCourse userCourse)
        {
            if (id != userCourse.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserCourses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserCourse>> PostUserCourse(UserCourse userCourse)
        {
            _context.UserCourses.Add(userCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserCourseExists(userCourse.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserCourse", new { id = userCourse.UserId }, userCourse);
        }

        // DELETE: api/UserCourses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserCourse>> DeleteUserCourse(Guid id)
        {
            var userCourse = await _context.UserCourses.FindAsync(id);
            if (userCourse == null)
            {
                return NotFound();
            }

            _context.UserCourses.Remove(userCourse);
            await _context.SaveChangesAsync();

            return userCourse;
        }
        [HttpGet("user/courses")]
        public IActionResult GetCoursesForUser()
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = GetUser(guidId);

            if (!UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            var items = _context.UserCourses
                .Include(uc => uc.Course)
                .Where(uc => uc.UserId == userFromToken.Id);
            //User user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return Ok(items);

        }
        [HttpGet("{courseId}/users")]
        public IActionResult GetUsersForCourse(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                return NotFound(new JsonResult("User not found"));
            }

            var items = _context.UserCourses
                .Include(uc => uc.User)
                .Where(uc => uc.CourseId == courseId);
            //User user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return Ok(items);

        }
        private bool UserCourseExists(Guid id)
        {
            return _context.UserCourses.Any(e => e.UserId == id);
        }
        public User GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _context.Users
                .FirstOrDefault(u => u.Id == userId);
        }
        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _context.Users.Any(a => a.Id == userId);
        }
    }
}

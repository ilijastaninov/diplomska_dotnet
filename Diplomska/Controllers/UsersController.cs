using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using AutoMapper;
using Diplomska.Context;
using Diplomska.DTOS.UsersDTO;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Diplomska.jwt.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diplomska.Controllers
{
    [EnableCors("CorsAPI")]
    [Authorize]
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly  IUserInterface repo;
        private readonly IMapper mapper;
        private readonly ConnectorDbContext _context;
        public UsersController(IUserInterface _repo,IMapper _mapper, ConnectorDbContext context)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = repo.GetUsers();
            return Ok(mapper.Map<IEnumerable<UserDto>>(users));
        }
        [HttpGet("user"/*"{userId}"*/,Name = "GetAuthor")]
        public ActionResult<UserDto> GetUser(/*Guid userId*/)
        {
            /*var user = repo.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserDto>(user));*/
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            return Ok(mapper.Map<UserDto>(userFromToken));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserDto> CreateUser(UserForCreationDto user)
        {
            var userEntity = mapper.Map<User>(user);
            if (repo.UsernameExists(userEntity.Username))
            {
                return BadRequest("Username already exists");
            }
            repo.AddUser(userEntity);
            repo.Save();

            var userToReturn = mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("GetAuthor", new {userId = userToReturn.Id}, userToReturn);
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateUser(Guid userId,UserToUpdateDto user)
        {
            
            var getUser = repo.GetUser(userId);
            if (getUser == null)
            {
                return NotFound();
            }

            var userEntity = mapper.Map(user,getUser);
            repo.UpdateUser(userEntity);
            repo.Save();
            return NoContent();
        }

        [HttpPatch("{userId}")]
        public ActionResult PatchUser(Guid userId, JsonPatchDocument<UserToUpdateDto> patchDocument)
        {
            var user = repo.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userToPatch = mapper.Map<UserToUpdateDto>(user);
            patchDocument.ApplyTo(userToPatch,ModelState);
            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(userToPatch, user);
            repo.UpdateUser(user);
            repo.Save();
            return NoContent();
        }

        [HttpDelete("deleteuser"/*"{userId}"*/)]
        public IActionResult DeleteUser(/*Guid userId*/)
        {
            /*var user = repo.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }*/
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            repo.DeleteUser(userFromToken);
            repo.Save();
            return NoContent();
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(JwtRequest jwtRequest)
        {
            var response = repo.Authenticate(jwtRequest);
            if (response == null)
            {
                return BadRequest(new {message = "Invalid credentials"});
            }

            return Ok(response);
        }

        /*[HttpGet("id")]
        public IActionResult GetUserByToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            return Ok(userFromToken);
        }*/

    }
}
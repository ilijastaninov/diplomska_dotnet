using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using Diplomska.Context;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diplomska.Controllers
{
    [EnableCors("CorsAPI")]
    [Authorize]
    [ApiController]
    [Route("/api/users/user/posts")]
    public class PostController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPostInterface repository;
        private readonly ConnectorDbContext context;

        public PostController(IPostInterface _repository, IMapper _mapper, ConnectorDbContext _context)
        {
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            repository = _repository ?? throw new ArgumentNullException(nameof(_repository));
            context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPostsForUser()
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repository.GetUser(guidId);

            if (!repository.UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            var postsForUser = repository.GetAllPostsForUser(userFromToken.Id);
            return Ok(/*mapper.Map<IEnumerable<PostDto>>(postsForUser)*/postsForUser);
        }
        [HttpGet("{postId}",Name = "GetPost")]
        public ActionResult<Post> GetPostById(Guid postId)
        {

            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repository.GetUser(guidId);

            if (!repository.UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            var postForUser = repository.GetPostById(userFromToken.Id, postId);
            if (postForUser == null)
            {
                return NotFound("Post does not exist");
            }

            return Ok(postForUser);
        }
        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repository.GetUser(guidId);

            if (!repository.UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            /*var educationEntity = mapper.Map<Education>(education);*/
            repository.AddPost(userFromToken.Id, post);
            repository.Save();

           // var educationToReturn = mapper.Map<EducationDto>(educationEntity);
            return CreatedAtRoute("GetPost", new
            {
                userFromToken.Id,
                postId = post.PostId
            }, post);
        }
        [HttpDelete("{postId}")]
        public ActionResult DeleteEducationForUser(Guid postId)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repository.GetUser(guidId);

            if (!repository.UserExists(userFromToken.Id))
            {
                return NotFound();
            }
            var postForUser = repository.GetPostById(userFromToken.Id, postId);
            if (postForUser == null)
            {
                return NotFound("POst not found");
            }

            repository.DeletePost(postForUser);
            repository.Save();
            return NoContent();
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<Post>> GetAllPosts ()
        {
            var postsForUser = //repository.GetAllPosts();
                context.Posts.Include(postsForUser => postsForUser.User).ToList();
            return Ok(/*mapper.Map<IEnumerable<PostDto>>(postsForUser)*/postsForUser);
        }

    }
}
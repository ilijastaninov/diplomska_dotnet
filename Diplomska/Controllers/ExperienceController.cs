using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using Diplomska.DTOS.EducationDTO;
using Diplomska.DTOS.ExperienceDTO;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Diplomska.Controllers
{
    [EnableCors("CorsAPI")]
    [Authorize]
    [ApiController]
    [Route("/api/users/user/experiences")]
    //[Route("/api/users/{userId}/experiences")]
    public class ExperienceController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IExperienceInterface repository;
        public ExperienceController(IExperienceInterface _repository, IMapper _mapper)
        {
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            repository = _repository ?? throw new ArgumentNullException(nameof(_repository));
        }
        [HttpGet]
        public ActionResult<IEnumerable<ExperienceDto>> GetEducationsForUser()
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

            var experiencesForUser = repository.GetAllExperiencesForUser(userFromToken.Id);
            return Ok(mapper.Map<IEnumerable<ExperienceDto>>(experiencesForUser));
        }
        [HttpGet("{experienceId}", Name = "GetExperienceForUser")]
        public ActionResult<ExperienceDto> GetExperienceForUser(Guid experienceId)
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
                return NotFound("User not found");
            }

            var experienceUser = repository.GetExperienceById(userFromToken.Id, experienceId);
            if (experienceUser == null)
            {
                return NotFound("Experience not found");
            }

            return Ok(mapper.Map<ExperienceDto>(experienceUser));
        }
        [HttpPost]
        public ActionResult<ExperienceDto> CreateExperienceForUser(ExperienceForCreation education)
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

            var experienceEntity = mapper.Map<Experience>(education);
            repository.AddExperience(userFromToken.Id, experienceEntity);
            repository.Save();

            var experienceToReturn = mapper.Map<ExperienceDto>(experienceEntity);
            return CreatedAtRoute("GetExperienceForUser", new
            {
                userFromToken.Id,
                experienceId = experienceToReturn.ExperienceId
            }, experienceToReturn);
        }
        [HttpPut("{experienceId}")]
        public ActionResult UpdateExperienceForUser(Guid experienceId, ExperienceToUpdate experience)
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
            var experienceForUser = repository.GetExperienceById(userFromToken.Id, experienceId);
            if (experienceForUser == null)
            {
                return NotFound();
            }
            // map the entity to educationForUpdateDto
            // apply the updated field values
            //map the educationForUpdateDto back to the entity
            mapper.Map(experience, experienceForUser);
            repository.UpdateExperience(experienceForUser);
            repository.Save();
            return NoContent();
        }
        [HttpDelete("{experienceId}")]
        public ActionResult DeleteExperienceForUser(Guid experienceId)
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
            var experienceForUser = repository.GetExperienceById(userFromToken.Id, experienceId);
            if (experienceForUser == null)
            {
                return NotFound();
            }
            repository.DeleteExperience(experienceForUser);
            repository.Save();
            return NoContent();
        }
    }
}
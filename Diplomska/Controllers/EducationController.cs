using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using Diplomska.DTOS.EducationDTO;
using Diplomska.Entities;
using Diplomska.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Diplomska.Controllers
{
    [EnableCors("CorsAPI")]
    [Authorize]
    [ApiController]
    [Route("/api/users/user/educations")]
    //[Route("/api/users/{userId}/educations")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationInterface repo;
        private readonly IUserInterface userRepo;
        private readonly IMapper mapper;
        public EducationController(IUserInterface _userRepo,IEducationInterface _repo, IMapper _mapper)
        {
            repo=_repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            userRepo = _userRepo ?? throw new ArgumentNullException(nameof(_userRepo));
        }
        [HttpGet]
        public ActionResult<IEnumerable<EducationDto>> GetEducationsForUser()//Guid userId)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);

            if (!userRepo.UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            var educationsForUser = repo.GetEducations(userFromToken.Id);
            return Ok(mapper.Map<IEnumerable<EducationDto>>(educationsForUser));
        }

        [HttpGet("{educationId}",Name = "GetEducationForUser")]
        public ActionResult<EducationDto> GetEducationForUser( Guid educationId)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            if (!userRepo.UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            var educationForUser = repo.GetEducation(userFromToken.Id, educationId);
            if (educationForUser == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<EducationDto>(educationForUser));
        }

        [HttpPost]
        public ActionResult<EducationDto> CreateEducationForUser(EducationForCreationDto education)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            if (!userRepo.UserExists(userFromToken.Id))
            {
                return NotFound();
            }

            var educationEntity = mapper.Map<Education>(education);
            repo.AddEducation(userFromToken.Id, educationEntity);
            repo.Save();

            var educationToReturn = mapper.Map<EducationDto>(educationEntity);
            return CreatedAtRoute("GetEducationForUser", new
            {
                userFromToken.Id,
                educationId = educationToReturn.EducationId 
            }, educationToReturn);
        }

        [HttpPut("{educationId}")]
        public ActionResult UpdateEducationForUser(Guid educationId,EducationToUpdateDto education)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            if (!userRepo.UserExists(userFromToken.Id))
            {
                return NotFound();
            }
            var educationForUser = repo.GetEducation(userFromToken.Id, educationId);
            if (educationForUser == null)
            {
                // add the education if it doesn't exists
                var educationToAdd = mapper.Map<Education>(education);
                educationToAdd.EducationId = educationId;
                repo.AddEducation(userFromToken.Id, educationToAdd);
                repo.Save();
                var educationToReturn = mapper.Map<EducationDto>(educationToAdd);
                return CreatedAtRoute("GetEducationForUser", new
                {
                    userFromToken.Id,
                    educationId = educationToReturn.EducationId
                }, educationToReturn);
            }
            // map the entity to educationForUpdateDto
            // apply the updated field values
            //map the educationForUpdateDto back to the entity
            mapper.Map(education, educationForUser);
            repo.UpdateEducation(educationForUser);
            repo.Save();
            return NoContent();
        }

        [HttpPatch("{educationId}")]
        public IActionResult PartiallyUpdateEducationForUser(Guid userId, Guid educationId,
            JsonPatchDocument<EducationToUpdateDto> educationDocument)
        {
            if (!userRepo.UserExists(userId))
            {
                return NotFound();
            }
            var educationForUser = repo.GetEducation(userId, educationId);
            if (educationForUser == null)
            {
                var educationDto = new EducationToUpdateDto();
                educationDocument.ApplyTo(educationDto,ModelState);
                if (!TryValidateModel(educationDto)) // code to add validation to patch plus ModelState in applyTo
                {
                    return ValidationProblem(ModelState);
                }
                var educationToAdd = mapper.Map<Education>(educationDto);
                educationToAdd.EducationId = educationId;

                repo.AddEducation(userId,educationToAdd);
                repo.Save();

                var educationToReturn = mapper.Map<EducationDto>(educationToAdd);
                return CreatedAtRoute("GetEducationForUser",

                    new {userId, educationId = educationToReturn.EducationId}, educationToReturn
                );
            }

            var educationToPatch = mapper.Map<EducationToUpdateDto>(educationForUser);
            educationDocument.ApplyTo(educationToPatch,ModelState);
            if (!TryValidateModel(educationToPatch))
            {
                return ValidationProblem(ModelState);
            }
            mapper.Map(educationToPatch, educationForUser);
            repo.UpdateEducation(educationForUser);
            repo.Save();
            return NoContent();
        }

        [HttpDelete("{educationId}")]
        public ActionResult DeleteEducationForUser(Guid educationId)
        {
            var handler = new JwtSecurityTokenHandler();
            var header = Request.Headers["Authorization"];
            var credValue = header.ToString().Substring("Bearer ".Length).Trim();
            var jsonToken = handler.ReadToken(credValue);
            var tokenS = handler.ReadToken(credValue) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            var guidId = Guid.Parse(id);
            var userFromToken = repo.GetUser(guidId);
            if (!userRepo.UserExists(userFromToken.Id))
            {
                return NotFound();
            }
            var educationForUser = repo.GetEducation(userFromToken.Id, educationId);
            if (educationForUser == null)
            {
                return NotFound();
            }
            repo.DeleteEducation(educationForUser);
            repo.Save();
            return NoContent();
        }
    }
}
using Admin.Api.Resources;
using Admin.Api.Vslidator;
using Admin.Core.Models;
using Admin.Core.Services;
using Admin.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly IMapper _mapper;

        public TutorController(ITutorService tutorService, IMapper mapper)
        {
            _tutorService = tutorService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("tutors")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<IEnumerable<TutorResource>>> GetAllTutors()
        {
            try
            {
                var tutors = await _tutorService.GetAllTutors();
                var tutorService = _mapper.Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors);

                string roleName = ""; // Define roleName

                // Retrieve roleName from the current user's claims
                var claimsPrincipal = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    roleName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                }

                if (roleName == "Administrator")
                {
                    return Ok(tutorService);
                }

                return Ok(tutorService);
            }
            catch (Exception ex)
            {
                Response.Headers.Add("Error", ex.Message);
                return NoContent();
            }
        }



        [HttpGet("{id}")]
        [ActionName("tutors")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<TutorResource>> GetTutorById(int id)
        {
            try
            {
                var tutor = await _tutorService.GetTutorById(id);
                if (tutor == null)
                {
                    return NotFound();
                }
                var tutorResourse = _mapper.Map<Tutor, TutorResource>(tutor);

                string roleName = ""; // Define roleName

                // Retrieve roleName from the current user's claims
                var claimsPrincipal = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    roleName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                }

                if (roleName == "Administrator")
                {
                    return Ok(tutorResourse);
                }

                return Ok(tutorResourse);
            }
            catch (Exception ex)
            {
                Response.Headers.Add("Error", ex.Message);
                return NoContent();
            }
        }



        [HttpPost("")]
        [ActionName("tutors")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<TutorResource>> CreateTutor([FromBody] SaveTutorResource saveTutorResource)
        {
            var validation = new SaveTutorResourceValidator();

            var validationResult = await validation.ValidateAsync(saveTutorResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var tutor = _mapper.Map<SaveTutorResource, Tutor>(saveTutorResource);

            var newTutor = await _tutorService.CreateTutor(tutor);

            var tutorCreated = await _tutorService.GetTutorById(newTutor.Id);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(tutorCreated);

            return Ok(tutorResource);
        }




        [HttpPut("{id}")]
        [ActionName("tutors")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<TutorResource>> UpdateTutor(int id, [FromBody] SaveTutorResource saveTutorResource)
        {
            // validation 
            var validation = new SaveTutorResourceValidator();
            var validationResult = await validation.ValidateAsync(saveTutorResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var tutorToUpdate = await _tutorService.GetTutorById(id);

            if (tutorToUpdate == null)
            {
                return NotFound();
            }
            var tutor = _mapper.Map<SaveTutorResource, Tutor>(saveTutorResource);

            await _tutorService.UpdateTutor(tutorToUpdate, tutor);

            var tutorUpdated = await _tutorService.GetTutorById(id);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(tutorUpdated);
            return Ok(tutorResource);
        }




        [HttpDelete("{id}")]
               [ActionName("tutors")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult> DeleteTutor(int id)
        {
            var tutor = await _tutorService.GetTutorById(id);
            if (tutor == null)
            {
                return NotFound();
            }

            await _tutorService.SoftDeleteTutor(tutor); // Use SoftDelete 
            return NoContent();
        }


    }
}

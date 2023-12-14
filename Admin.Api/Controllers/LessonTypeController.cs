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
    public class LessonTypeController : ControllerBase
    {

        private readonly ILessonTypeService _lessontypeService;
        private readonly IMapper _mapper;

        public LessonTypeController(ILessonTypeService lessontypeService, IMapper mapper)
        {
            _lessontypeService = lessontypeService;
            _mapper = mapper;
        }



        [HttpGet("")]
        [ActionName("GetAllLessonTypes")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]

        public async Task<ActionResult<IEnumerable<LessonTypeResource>>> GetAllLessonTypes()
        {
            var lessontypes = await _lessontypeService.GetAllLessonTypes();
            var lessontypeResource = _mapper.Map<IEnumerable<LessonType>, IEnumerable<LessonTypeResource>>(lessontypes);
            return Ok(lessontypeResource);
        }





        [HttpGet("{id}")]
        [ActionName("GetLessonTypeById")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]

        public async Task<ActionResult<LessonTypeResource>> GetLessonTypeById(int id)
        {
            var lessontype = await _lessontypeService.GetLessonTypeById(id);
            if (lessontype == null)
            {
                return NotFound();
            }
            var artistResource = _mapper.Map<LessonType, LessonTypeResource>(lessontype);
            return Ok(artistResource);
        }





        [HttpPost("")]
        [ActionName("CreateLessonType")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<LessonTypeResource>> CreateLessonType([FromBody] SaveLessonTypeResource saveLessonTypeResource)
        {
            var validation = new SaveLessonTypeResourceValidator();

            var validationResult = await validation.ValidateAsync(saveLessonTypeResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var lessontype = _mapper.Map<SaveLessonTypeResource, LessonType>(saveLessonTypeResource);

            var newLessonType = await _lessontypeService.CreateLessonType(lessontype);

            var lessontypeCreated = await _lessontypeService.GetLessonTypeById(newLessonType.Id);

            var lessontypeResource = _mapper.Map<LessonType, LessonTypeResource>(lessontypeCreated);

            return Ok(lessontypeResource);
        }





        [HttpPut("{id}")]
        [ActionName("UpdateLessonType")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<LessonTypeResource>> UpdateLessonType(int id, [FromBody] SaveLessonTypeResource saveLessonTypeResource)
        {
            // validation 
            var validation = new SaveLessonTypeResourceValidator();
            var validationResult = await validation.ValidateAsync(saveLessonTypeResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var lessontypeToUpdate = await _lessontypeService.GetLessonTypeById(id);

            if (lessontypeToUpdate == null)
            {
                return NotFound();
            }
            var lessontype = _mapper.Map<SaveLessonTypeResource, LessonType>(saveLessonTypeResource);

            await _lessontypeService.UpdateLessonType(lessontypeToUpdate, lessontype);

            var lessontypeUpdated = await _lessontypeService.GetLessonTypeById(id);

            var lessontypeResource = _mapper.Map<LessonType, LessonTypeResource>(lessontypeUpdated);
            return Ok(lessontypeResource);
        }








        [HttpDelete("{id}")]
        [ActionName("DeleteLessonType")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult> DeleteLessonType(int id)
        {
            var lessontype = await _lessontypeService.GetLessonTypeById(id);
            if (lessontype == null)
            {
                return NotFound();
            }

            await _lessontypeService.SoftDeleteLessonType(lessontype); // Use SoftDelete 
            return NoContent();
        }




    }
}

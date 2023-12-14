using Admin.Api.Resources;
using Admin.Api.Vslidator;
using Admin.Core.Models;
using Admin.Core.Services;
using Admin.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IServiceService _serviceService;
        private readonly ITutorService _tutorService;
        private readonly ILessonTypeService _lessontypeService;
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;


        public GroupController(IGroupService groupService, IMapper mapper, IBranchService branchService,  ILessonTypeService lessontypeService, ITutorService tutorService, IServiceService serviceService)
        {
            _groupService = groupService;
            _serviceService = serviceService;
            _tutorService = tutorService;
            _lessontypeService = lessontypeService;
            _branchService = branchService;
            _mapper = mapper;
        }



        [HttpGet("")]
        [ActionName("GetAllGroups")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]

        public async Task<ActionResult<IEnumerable<GroupResource>>> GetAllGroups()
        {
            var groups = await _groupService.GetAllWithService();

            var groupService = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return Ok(groupService);
        }




        [HttpGet("{id}")]
        [ActionName("GetGroupById")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<GroupResource>> GetGroupById(int id)
        {
            var group = await _groupService.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            var groupResource = _mapper.Map<Group, GroupResource>(group);
            return Ok(groupResource);


        }



        [HttpPost]
        [ActionName("CreateGroup")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<GroupResource>> CreateGroup([FromBody] SaveGroupResource groupSaveResource)
        {
            if (groupSaveResource == null)
            {
                return BadRequest("The request body is empty.");
            }

            var validatorGroup = new SaveGroupResourceValidator();
            var validationResult = await validatorGroup.ValidateAsync(groupSaveResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var group = _mapper.Map<SaveGroupResource, Group>(groupSaveResource);
            var newGroup = await _groupService.CreateGroup(group);
            return Ok(newGroup);
        }




        [HttpPut("{id}")]
        [ActionName("UpdateGroup")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<GroupResource>> UpdateGroup(int id, [FromBody] SaveGroupResource updateSaveResource)
        {
            if (updateSaveResource == null)
            {
                return BadRequest("The request body is empty.");
            }

            var validator = new SaveGroupResourceValidator();
            var resultValidato = await validator.ValidateAsync(updateSaveResource);

            if (!resultValidato.IsValid)
            {
                return BadRequest(resultValidato.Errors);
            }

            var groupToBeUpdate = await _groupService.GetGroupById(id);

            if (groupToBeUpdate == null)
            {
                return NotFound();
            }

            var groupUpdate = _mapper.Map<SaveGroupResource, Group>(updateSaveResource);
            await _groupService.UpdateGroup(groupToBeUpdate, groupUpdate);

            var groupNewUpdate = await _groupService.GetGroupById(id);
            var groupUpdateResource = _mapper.Map<Group, GroupResource>(groupNewUpdate);
            return Ok(groupUpdateResource);
        }




        [HttpDelete("{id}")]
        [ActionName("DeleteGroup")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]

        public async Task<ActionResult> DeleteGroup(int id)
        {
            var group = await _groupService.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            await _groupService.SoftDeleteGroup(group); // Use SoftDelete 
            return NoContent();
        }



        [HttpGet("Service/id")]
        [ActionName("GetAllGroupsServiceId")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<IEnumerable<GroupResource>>> GetAllGroupsServiceId(int id)
        {
            var service = await _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }
            var groups = await _groupService.GetGroupsByServiceId(id);
            var groupResources = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return Ok(groupResources);
        }


        [HttpGet("Tutor/id")]
        [ActionName("GetAllGroupsTutorId")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<IEnumerable<GroupResource>>> GetAllGroupsTutorId(int id)
        {
            var tutor = await _tutorService.GetTutorById(id);
            if (tutor == null)
            {
                return NotFound();
            }
            var groups = await _groupService.GetGroupsByTutorId(id);
            var groupResources = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return Ok(groupResources);
        }



        [HttpGet("LessonType/id")]
        [ActionName("GetAllGroupsLessonTypeId")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<IEnumerable<GroupResource>>> GetAllGroupsLessonTypeId(int id)
        {
            var lessontype = await _lessontypeService.GetLessonTypeById(id);
            if (lessontype == null)
            {
                return NotFound();
            }
            var groups = await _groupService.GetGroupsByLessonTypeId(id);
            var groupResources = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return Ok(groupResources);
        }



    }
}

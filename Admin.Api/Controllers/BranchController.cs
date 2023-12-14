using Admin.Api.Resources;
using Admin.Api.Vslidator;
using Admin.Core.Models;
using Admin.Core.Services;
using Admin.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;

        public BranchController(IBranchService branchService, IMapper mapper)
        {
            _branchService = branchService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("branchs")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<IEnumerable<BranchResource>>> GetAllBranchs()
        {
            try
            {
                var branchs = await _branchService.GetAllBranchs();
                var branchService = _mapper.Map<IEnumerable<Branch>, IEnumerable<BranchResource>>(branchs);

                string roleName = ""; // Define roleName

                // Retrieve roleName from the current user's claims
                var claimsPrincipal = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    roleName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                }

                if (roleName == "Administrator")
                {
                    return Ok(branchService);
                }

                return Ok(branchService);
            }
            catch (Exception ex)
            {
                Response.Headers.Add("Error", ex.Message);
                return NoContent();
            }
        }


        [HttpGet("{id}")]
        [ActionName("branchs")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<BranchResource>> GetBranchById(int id)
        {
            try
            {
                var branch = await _branchService.GetBranchById(id);
                if (branch == null)
                {
                    return NotFound();
                }
                var branchResourse = _mapper.Map<Branch, BranchResource>(branch);

                string roleName = ""; // Define roleName

                // Retrieve roleName from the current user's claims
                var claimsPrincipal = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    roleName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                }

                if (roleName == "Administrator")
                {
                    return Ok(branchResourse);
                }

                return Ok(branchResourse);
            }
            catch (Exception ex)
            {
                Response.Headers.Add("Error", ex.Message);
                return NoContent();
            }
        }


        [HttpPost("")]
        [ActionName("branchs")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<BranchResource>> CreateBranch([FromBody] SaveBranchResource saveBranchResource)
        {
            var validation = new SaveBranchResourceValidator();

            var validationResult = await validation.ValidateAsync(saveBranchResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var branch = _mapper.Map<SaveBranchResource, Branch>(saveBranchResource);

            var newBranch = await _branchService.CreateBranch(branch);

            var branchCreated = await _branchService.GetBranchById(newBranch.Id);

            var branchResource = _mapper.Map<Branch, BranchResource>(branchCreated);

            return Ok(branchResource);
        }



        [HttpPut("{id}")]
        [ActionName("branchs")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<BranchResource>> UpdateBranch(int id, [FromBody] SaveBranchResource saveBranchResource)
        {
            // validation 
            var validation = new SaveBranchResourceValidator();
            var validationResult = await validation.ValidateAsync(saveBranchResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var branchToUpdate = await _branchService.GetBranchById(id);

            if (branchToUpdate == null)
            {
                return NotFound();
            }
            var branch = _mapper.Map<SaveBranchResource, Branch>(saveBranchResource);

            await _branchService.UpdateBranch(branchToUpdate, branch);

            var branchUpdated = await _branchService.GetBranchById(id);

            var branchResource = _mapper.Map<Branch, BranchResource>(branchUpdated);
            return Ok(branchResource);
        }


        [HttpDelete("{id}")]
        [ActionName("branchs")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            var branch = await _branchService.GetBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }

            await _branchService.SoftDeleteBranch(branch); // Use SoftDelete 
            return NoContent();
        }


    }
}

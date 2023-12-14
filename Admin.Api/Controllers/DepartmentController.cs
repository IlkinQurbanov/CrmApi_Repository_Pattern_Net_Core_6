using Admin.Api.Resources;
using Admin.Api.Vslidator;
using Admin.Core.Models;
using Admin.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {


        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }








        [HttpPost("")]
        [ActionName("departments")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<DepartmentResource>> CreateDepartment([FromBody] SaveDepartmentResouorce saveDepartmentResource)
        {
            var validation = new SaveDepartmentResourceValidator();

            var validationResult = await validation.ValidateAsync(saveDepartmentResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var department = _mapper.Map<SaveDepartmentResouorce, Department>(saveDepartmentResource);

            var newDepartment = await _departmentService.CreateDepartment(department);

            var departmentCreated = await _departmentService.GetDepartmentById(newDepartment.Id);

            var departmentResource = _mapper.Map<Department, DepartmentResource>(departmentCreated);

            return Ok(departmentResource);
        }

    }
}

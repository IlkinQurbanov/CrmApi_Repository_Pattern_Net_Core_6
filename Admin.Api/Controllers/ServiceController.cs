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
    public class ServiceController : ControllerBase
    {

        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }



        [HttpGet("")]
        [ActionName("GetAllServices")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<IEnumerable<ServiceResource>>> GetAllServices()
        {
            var services = await _serviceService.GetAllServices();
            var serviceService = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return Ok(serviceService);
        }




        [HttpGet("{id}")]
        [ActionName("GetServiceById")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<ServiceResource>> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }
            var serviceResource = _mapper.Map<Service, ServiceResource>(service);
            return Ok(serviceResource);
        }



        [HttpPost("")]
        [ActionName("CreateServise")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<ServiceResource>> CreateServise([FromBody] SaveServiceResource saveServiceResource)
        {
            var validation = new SaveServiceResourceValidator();

            var validationResult = await validation.ValidateAsync(saveServiceResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var service = _mapper.Map<SaveServiceResource, Service>(saveServiceResource);

            var newService = await _serviceService.CreateService(service);

            var serviceCreated = await _serviceService.GetServiceById(newService.Id);

            var serviceResource = _mapper.Map<Service, ServiceResource>(serviceCreated);

            return Ok(serviceResource);
        }


        [HttpPut("{id}")]
        [ActionName("UpdateService")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult<ServiceResource>> UpdateService(int id, [FromBody] SaveServiceResource saveServiceResource)
        {
            // validation 
            var validation = new SaveServiceResourceValidator();
            var validationResult = await validation.ValidateAsync(saveServiceResource);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var serviceToUpdate = await _serviceService.GetServiceById(id);

            if (serviceToUpdate == null)
            {
                return NotFound();
            }
            var service = _mapper.Map<SaveServiceResource, Service>(saveServiceResource);

            await _serviceService.UpdateService(serviceToUpdate, service);

            var serviceUpdated = await _serviceService.GetServiceById(id);

            var serviceResource = _mapper.Map<Service, ServiceResource>(serviceUpdated);
            return Ok(serviceResource);
        }



        [HttpDelete("{id}")]
        [ActionName("UpdateService")]
        [Authorize(Policy = "AdminManagerClerkPolicy")]
        public async Task<ActionResult> DeleteService(int id)
        {
            var service = await _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            await _serviceService.SoftDeleteService(service); // Use SoftDelete 
            return NoContent();
        }

    }
}

using Admin.Core.Models;
using Admin.Core.Services;
using Admin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Service> CreateService(Service newService)
        {
            await _unitOfWork.Services
                .AddAsync(newService);
            await _unitOfWork.CommitAsync();

            return newService;
        }

        //Simple way to delete
        //public async Task DeleteService(Service service)
        //{
        //    _unitOfWork.Services.Remove(service);

        //    await _unitOfWork.CommitAsync();
        //}


        public async Task DeleteService(Service service)
        {
            service.IsDeleted = true;
            service.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task SoftDeleteService(Service service)
        {
            service.IsDeleted = true;
            service.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<Service>> GetAllServices()
        {
            return await _unitOfWork.Services.GetAllAsync();
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await _unitOfWork.Services.GetByIdAsync(id);
        }

        public async Task UpdateService(Service serviceToBeUpdated, Service service)
        {
            serviceToBeUpdated.Name = service.Name;

            await _unitOfWork.CommitAsync();
        }

    }
}

using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllServices();
        Task<Service> GetServiceById(int id);
        Task<Service> CreateService(Service newService);
        Task UpdateService(Service serviceToBeUpdated, Service newService);
        Task DeleteService(Service service);
        Task SoftDeleteService(Service service); // Soft Delete method
    }
}

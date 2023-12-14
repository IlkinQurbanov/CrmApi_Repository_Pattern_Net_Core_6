using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public interface IServiceRepository : ICrmRepository<Service>
    {
        Task<IEnumerable<Service>> GetAllWithGroupsAsync();
        Task<Service> GetWithGroupsByIdAsync(int id);
        Task<IEnumerable<Service>> GetAllWithoutDeletedAsync(); // Soft delete method
    }
}

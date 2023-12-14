using Admin.Core.Models;
using Admin.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dsta.Repositories
{

    public class ServiceRepository : CrmRepository<Service>, IServiceRepository
    {
        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public ServiceRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Service>> GetAllWithServicesAsync()
        {
            return await DataContext.Services
                .Include(a => a.Groups)
                .ToListAsync();
        }

        public Task<Service> GetWithServicesByIdAsync(int id)
        {
            return DataContext.Services
                .Include(a => a.Groups)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        async Task<IEnumerable<Service>> IServiceRepository.GetAllWithGroupsAsync()
        {
            return await DataContext.Services
              .Include(a => a.Groups)
              .ToListAsync();
        }

        Task<Service> IServiceRepository.GetWithGroupsByIdAsync(int id)
        {
            return DataContext.Services
          .Include(a => a.Groups)
         .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Service>> GetAllWithoutDeletedAsync()
        {
            return await DataContext.Services
                .Where(b => !b.IsDeleted) // Filter out deleted 
                .ToListAsync();
        }

    }
}

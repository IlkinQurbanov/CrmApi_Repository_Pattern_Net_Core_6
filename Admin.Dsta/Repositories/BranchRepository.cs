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
    public class BranchRepository : CrmRepository<Branch>, IBranchRepository
    {
        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public BranchRepository(DataContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Branch>> GetAllWithBranchsAsync()
        {
            return await DataContext.Branchs
                .Include(a => a.Groups)
                .ToListAsync();
        }

        public Task<Branch> GetWithBranchByIdAsync(int id)
        {
            return DataContext.Branchs
                .Include(a => a.Groups)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        async Task<IEnumerable<Branch>> IBranchRepository.GetAllWithGroupsAsync()
        {
            return await DataContext.Branchs
              .Include(a => a.Groups)
              .ToListAsync();
        }

        Task<Branch> IBranchRepository.GetWithGroupsByIdAsync(int id)
        {
            return DataContext.Branchs
          .Include(a => a.Groups)
         .SingleOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Branch>> GetAllWithoutDeletedAsync()
        {
            return await DataContext.Branchs
                .Where(b => !b.IsDeleted) // Filter out deleted branches
                .ToListAsync();
        }

    }
}

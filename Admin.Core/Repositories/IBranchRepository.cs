using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public interface IBranchRepository : ICrmRepository<Branch>
    {
        Task<IEnumerable<Branch>> GetAllWithGroupsAsync();
        Task<Branch> GetWithGroupsByIdAsync(int id);
        Task<IEnumerable<Branch>> GetAllWithoutDeletedAsync(); // Soft delete method
    }

}

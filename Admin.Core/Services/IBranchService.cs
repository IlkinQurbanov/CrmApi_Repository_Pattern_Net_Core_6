using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{
    public interface IBranchService
    {
        Task<IEnumerable<Branch>> GetAllBranchs();
        Task<Branch> GetBranchById(int id);
        Task<Branch> CreateBranch(Branch newBranch);
        Task UpdateBranch(Branch branchToBeUpdated, Branch newBranch);
        Task DeleteBranch(Branch branch);
        Task SoftDeleteBranch(Branch branch); // Soft Delete method
    }

}

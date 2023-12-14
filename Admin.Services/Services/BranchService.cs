using Admin.Core;
using Admin.Core.Models;
using Admin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Admin.Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Branch>> GetAllBranchs()
        {
            return await _unitOfWork.Branchs.GetAllWithoutDeletedAsync();
        }


        public async Task<Branch> GetBranchById(int id)
        {
            return await _unitOfWork.Branchs.GetByIdAsync(id);
        }

        public async Task<Branch> CreateBranch(Branch newBranch)
        {
            await _unitOfWork.Branchs.AddAsync(newBranch);
            await _unitOfWork.CommitAsync();

            return newBranch;
        }

        public async Task UpdateBranch(Branch branchToBeUpdated, Branch branch)
        {
            branchToBeUpdated.Name = branch.Name;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBranch(Branch branch)
        {
            branch.IsDeleted = true;
            branch.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task SoftDeleteBranch(Branch branch)
        {
            branch.IsDeleted = true;
            branch.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }
    }

}

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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _unitOfWork.Departments.GetAllWithoutDeletedAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _unitOfWork.Departments.GetByIdAsync(id);
        }

        public async Task<Department> CreateDepartment(Department newDepartment)
        {
            await _unitOfWork.Departments.AddAsync(newDepartment);
            await _unitOfWork.CommitAsync();

            return newDepartment;
        }

        public async Task UpdateDepartment(Department departmentToBeUpdated, Department department)
        {
            departmentToBeUpdated.Name = department.Name;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteDepartment(Department department)
        {
            department.IsDeleted = true;
            department.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task SoftDeleteDepartment(Department department)
        {
            department.IsDeleted = true;
            department.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }
    }
}

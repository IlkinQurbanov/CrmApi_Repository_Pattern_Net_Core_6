using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> CreateDepartment(Department newDepartment);
        Task UpdateDepartment(Department departmentToBeUpdated, Department newDepartment);
        Task DeleteDepartment(Department department);
        Task SoftDeleteDepartment(Department department); // Soft Delete method
    }
}

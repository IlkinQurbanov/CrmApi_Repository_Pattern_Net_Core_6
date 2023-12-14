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
    public class DepartmentRepository : CrmRepository<Department>, IDepartmentRepository
    {
        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public DepartmentRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> GetAllWithDepartmentsAsync()
        {
            return await DataContext.Departments
                .ToListAsync();
        }

        public Task<Department> GetWithDepartmentByIdAsync(int id)
        {
            return DataContext.Departments
                .SingleOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Department>> GetAllWithoutDeletedAsync()
        {
            return await DataContext.Departments
                .Where(b => !b.IsDeleted) // Filter out deleted 
                .ToListAsync();
        }
    }
}

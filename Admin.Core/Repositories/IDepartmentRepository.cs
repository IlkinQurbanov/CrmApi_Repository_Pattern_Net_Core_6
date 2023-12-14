using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public interface IDepartmentRepository : ICrmRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllWithoutDeletedAsync(); // Soft delete method

    }
}

using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public  interface ITutorRepository :ICrmRepository<Tutor>
    {
        Task<IEnumerable<Tutor>> GetAllWithGroupsAsync();
        Task<Tutor> GetWithGroupsByIdAsync(int id);
        Task<IEnumerable<Tutor>> GetAllWithoutDeletedAsync(); // Soft delete method
    }
}

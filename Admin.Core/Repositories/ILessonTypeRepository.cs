using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public interface ILessonTypeRepository : ICrmRepository<LessonType>
    {
        Task<IEnumerable<LessonType>> GetAllWithGroupsAsync();
        Task<LessonType> GetWithGroupsByIdAsync(int id);
        Task<IEnumerable<LessonType>> GetAllWithoutDeletedAsync(); // Soft delete method
    }
}

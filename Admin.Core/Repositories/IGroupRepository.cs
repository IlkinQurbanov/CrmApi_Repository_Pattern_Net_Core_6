using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public interface IGroupRepository : ICrmRepository<Group>
    {
        Task<IEnumerable<Group>> GetAllWithServiceAsync();
        Task<Group> GetWithServiceByIdAsync(int id);
        Task<IEnumerable<Group>> GetAllWithServiceByServiceIdAsync(int serviceId);

        Task<IEnumerable<Group>> GetAllWithTutorAsync();
        Task<Group> GetWithTutorByIdAsync(int id);
        Task<IEnumerable<Group>> GetAllWithTutorByTutorIdAsync(int tutorId);

        Task<IEnumerable<Group>> GetAllWithLessonTypeAsync();
        Task<Group> GetWithLessonTypeByIdAsync(int id);
        Task<IEnumerable<Group>> GetAllWithLessonTypeByLessonTypeIdAsync(int lessontypeId);
        Task<IEnumerable<Group>> GetAllWithoutDeletedAsync(); // Soft delete method
    }
}

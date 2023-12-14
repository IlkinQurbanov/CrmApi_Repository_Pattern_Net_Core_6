using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{

    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllGroups();
        Task<IEnumerable<Group>> GetAllWithTutor();
        Task<IEnumerable<Group>> GetAllWithService();
        Task<IEnumerable<Group>> GetAllWithLessonType();
        Task<Group> GetGroupById(int id);
        Task<IEnumerable<Group>> GetGroupsByServiceId(int serviceId);
        Task<IEnumerable<Group>> GetGroupsByTutorId(int tutorId);
        Task<IEnumerable<Group>> GetGroupsByLessonTypeId(int lessontypeId);
        Task<Group> CreateGroup(Group newGroup);
        Task UpdateGroup(Group groupToBeUpdated, Group group);
        Task DeleteGroup(Group group);
        Task SoftDeleteGroup(Group group); // Soft Delete method
    }
}

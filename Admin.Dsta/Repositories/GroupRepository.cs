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
    public class GroupRepository : CrmRepository<Group>, IGroupRepository
    {

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public GroupRepository(DataContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Group>> GetAllWithServiceAsync()
        {
            return await DataContext.Groups
                .Include(g => g.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithTutorAsync()
        {
            return await DataContext.Groups
                .Include(g => g.Tutor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithLessonTypeAsync()
        {
            return await DataContext.Groups
                .Include(g => g.LessonType)
                .ToListAsync();
        }

        public async Task<Group> GetWithServiceByIdAsync(int id)
        {
            return await DataContext.Groups
                .Include(g => g.Service)
                .SingleOrDefaultAsync(g => g.Id == id); ;
        }

        public async Task<Group> GetWithTutorByIdAsync(int id)
        {
            return await DataContext.Groups
                .Include(g => g.Tutor)
                .SingleOrDefaultAsync(g => g.Id == id); ;
        }

        public async Task<Group> GetWithLessonTypeByIdAsync(int id)
        {
            return await DataContext.Groups
                .Include(g => g.LessonType)
                .SingleOrDefaultAsync(g => g.Id == id); ;
        }

        public async Task<IEnumerable<Group>> GetAllWithServiceByServiceIdAsync(int serviceId)
        {
            return await DataContext.Groups
                .Include(g => g.Service)
                .Where(g => g.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithTutorByTutorIdAsync(int tutorId)
        {
            return await DataContext.Groups
                .Include(g => g.Tutor)
                .Where(g => g.TutorId == tutorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithLessonTypeByLessonTypeIdAsync(int lessontypeId)
        {
            return await DataContext.Groups
                .Include(g => g.LessonType)
                .Where(g => g.LessonTypeId == lessontypeId)
                .ToListAsync();
        }

        async Task<IEnumerable<Group>> IGroupRepository.GetAllWithServiceAsync()
        {
            return await DataContext.Groups
              .Include(g => g.Service)
              .ToListAsync();
        }

        async Task<IEnumerable<Group>> IGroupRepository.GetAllWithTutorAsync()
        {
            return await DataContext.Groups
              .Include(g => g.Tutor)
              .ToListAsync();
        }

        async Task<IEnumerable<Group>> IGroupRepository.GetAllWithLessonTypeAsync()
        {
            return await DataContext.Groups
              .Include(g => g.LessonType)
              .ToListAsync();
        }

        async Task<Group> IGroupRepository.GetWithServiceByIdAsync(int id)
        {
            return await DataContext.Groups
               .Include(g => g.Service)
               .SingleOrDefaultAsync(g => g.Id == id); ;
        }

        async Task<Group> IGroupRepository.GetWithTutorByIdAsync(int id)
        {
            return await DataContext.Groups
               .Include(g => g.Tutor)
               .SingleOrDefaultAsync(g => g.Id == id); ;
        }

        async Task<Group> IGroupRepository.GetWithLessonTypeByIdAsync(int id)
        {
            return await DataContext.Groups
               .Include(g => g.LessonType)
               .SingleOrDefaultAsync(g => g.Id == id); ;
        }

        async Task<IEnumerable<Group>> IGroupRepository.GetAllWithServiceByServiceIdAsync(int serviceId)
        {
            return await DataContext.Groups
               .Include(g => g.Service)
               .Where(g => g.ServiceId == serviceId)
               .ToListAsync();
        }
        async Task<IEnumerable<Group>> IGroupRepository.GetAllWithLessonTypeByLessonTypeIdAsync(int lessontypeId)
        {
            return await DataContext.Groups
               .Include(g => g.LessonType)
               .Where(g => g.ServiceId == lessontypeId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithoutDeletedAsync()
        {
            return await DataContext.Groups
                .Where(b => !b.IsDeleted) // Filter out deleted 
                .ToListAsync();
        }

      

    }
}

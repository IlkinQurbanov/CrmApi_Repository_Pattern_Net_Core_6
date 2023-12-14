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
    public class LessonTypeRepository : CrmRepository<LessonType>, ILessonTypeRepository
    {

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public LessonTypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LessonType>> GetAllWithLessonTypesAsync()
        {
            return await DataContext.LessonTypes
                .Include(a => a.Groups)
                .ToListAsync();
        }

        public Task<LessonType> GetWithLessonTypesByIdAsync(int id)
        {
            return DataContext.LessonTypes
                .Include(a => a.Groups)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        async Task<IEnumerable<LessonType>> ILessonTypeRepository.GetAllWithGroupsAsync()
        {
            return await DataContext.LessonTypes
              .Include(a => a.Groups)
              .ToListAsync();
        }

        Task<LessonType> ILessonTypeRepository.GetWithGroupsByIdAsync(int id)
        {
            return DataContext.LessonTypes
          .Include(a => a.Groups)
         .SingleOrDefaultAsync(a => a.Id == id);
        }


        public async Task<IEnumerable<LessonType>> GetAllWithoutDeletedAsync()
        {
            return await DataContext.LessonTypes
                .Where(b => !b.IsDeleted) // Filter out deleted 
                .ToListAsync();
        }



    }
}

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
    public class TutorRepository : CrmRepository<Tutor>, ITutorRepository
    {
        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public TutorRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tutor>> GetAllWithTutorsAsync()
        {
            return await DataContext.Tutors
                .Include(a => a.Groups)
                .ToListAsync();
        }

        public Task<Tutor> GetWithTutorsByIdAsync(int id)
        {
            return DataContext.Tutors
                .Include(a => a.Groups)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        async Task<IEnumerable<Tutor>> ITutorRepository.GetAllWithGroupsAsync()
        {
            return await DataContext.Tutors
              .Include(a => a.Groups)
              .ToListAsync();
        }

        Task<Tutor> ITutorRepository.GetWithGroupsByIdAsync(int id)
        {
            return DataContext.Tutors
          .Include(a => a.Groups)
         .SingleOrDefaultAsync(a => a.Id == id);
        }


        public async Task<IEnumerable<Tutor>> GetAllWithoutDeletedAsync()
        {
            return await DataContext.Tutors
                .Where(b => !b.IsDeleted) // Filter out deleted 
                .ToListAsync();
        }

    }
}

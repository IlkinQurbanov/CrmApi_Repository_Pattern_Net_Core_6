using Admin.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; }
        ITutorRepository Tutors { get; }

       ILessonTypeRepository LessonTypes { get; }
       IGroupRepository Groups { get; }
       IBranchRepository Branchs { get; }
       IDepartmentRepository Departments { get; }

        Task<int> CommitAsync();
    }
}

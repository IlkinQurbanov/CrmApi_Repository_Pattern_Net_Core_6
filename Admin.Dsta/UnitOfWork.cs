using Admin.Core.Repositories;
using Admin.Core;
using Admin.Dsta.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dsta
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        private ILessonTypeRepository _lessontypeRepository;
        private IGroupRepository _groupRepository;
        private IBranchRepository _branchRepository;
        private IDepartmentRepository _departmentRepository;
   

        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public IServiceRepository Services => _serviceRepository = _serviceRepository ?? new ServiceRepository(_context);
        public ITutorRepository Tutors => _tutorRepository = _tutorRepository ?? new TutorRepository(_context);
       public ILessonTypeRepository LessonTypes => _lessontypeRepository = _lessontypeRepository ?? new LessonTypeRepository(_context);
        public IGroupRepository Groups => _groupRepository = _groupRepository ?? new GroupRepository(_context);
        public IBranchRepository Branchs => _branchRepository = _branchRepository ?? new BranchRepository(_context);
        public IDepartmentRepository Departments => _departmentRepository = _departmentRepository ?? new DepartmentRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

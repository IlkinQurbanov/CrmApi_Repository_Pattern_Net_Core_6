using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Repositories
{
    public interface ICrmRepository<TEnity> where TEnity : class
    {

        ValueTask<TEnity> GetByIdAsync(int id);
        Task<IEnumerable<TEnity>> GetAllAsync();
        IEnumerable<TEnity> Find(Expression<Func<TEnity, bool>> predicate);
        Task<TEnity> SingleOrDefaultAsync(Expression<Func<TEnity, bool>> predicate);
        Task AddAsync(TEnity entity);
        Task AddRangeAsync(IEnumerable<TEnity> entities);
        void Remove(TEnity entity);
        void RemoveRange(IEnumerable<TEnity> entities);
    }
}

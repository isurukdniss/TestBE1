using CafeEmployeeManagement.Domain.Entities;
using CafeEmployeeManagement.Domain.Interfaces;
using CafeEmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CafeEmployeeManagement.Infrastructure.Repositories
{
    public class CafeRepository : Repository<Cafe, Guid>, ICafeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CafeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Cafe>> GetAllAsync(Expression<Func<Cafe, bool>>? filter)
        {
            IQueryable<Cafe> query = dbContext.Cafes.Include(x => x.Employees);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Cafe> GetByIdAsync(Guid id)
        {
            IQueryable<Cafe> query = dbContext.Cafes.Include(x => x.Employees);

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

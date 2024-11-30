using CafeEmployeeManagement.Domain.Entities;
using CafeEmployeeManagement.Domain.Interfaces;
using CafeEmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CafeEmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee, string>, IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee, bool>>? filter)
        {
            IQueryable<Employee> query = dbContext.Employees.Include(x => x.Cafe);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            IQueryable<Employee> query = dbContext.Employees.Include(x => x.Cafe);

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

using CafeEmployeeManagement.Application.Common.Models;
using MediatR;

namespace CafeEmployeeManagement.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<ApiResponse<IEnumerable<EmployeeDto>>>
    {
        public Guid? CafeId { get; set; }
    }
}

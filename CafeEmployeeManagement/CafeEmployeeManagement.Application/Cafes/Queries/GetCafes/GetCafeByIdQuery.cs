using CafeEmployeeManagement.Application.Common.Models;
using CafeEmployeeManagement.Application.Employees.Queries.GetEmployees;
using MediatR;

namespace CafeEmployeeManagement.Application.Cafes.Queries.GetCafes
{
    public class GetCafeByIdQuery : IRequest<ApiResponse<CafeResponseDto>>
    {
        public Guid CafeId { get; set; }
    }

}

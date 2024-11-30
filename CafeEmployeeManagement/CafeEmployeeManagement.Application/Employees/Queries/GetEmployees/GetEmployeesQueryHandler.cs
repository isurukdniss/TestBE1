using AutoMapper;
using CafeEmployeeManagement.Application.Common.Models;
using CafeEmployeeManagement.Domain.Entities;
using CafeEmployeeManagement.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace CafeEmployeeManagement.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, ApiResponse<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeDto>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {

            Expression<Func<Employee, bool>> filter = null;

            if (request.CafeId.HasValue)
            {
                filter = x => x.CafeId == request.CafeId.Value;
            }

            var employees = await employeeRepository.GetAllAsync(filter);

            if (employees == null)
            {
                return ApiResponse<IEnumerable<EmployeeDto>>.SetFailure(["Employee not found"]);
            }

            var employeeList = mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return ApiResponse<IEnumerable<EmployeeDto>>.SetSuccess(employeeList);
                
        }
    }
}

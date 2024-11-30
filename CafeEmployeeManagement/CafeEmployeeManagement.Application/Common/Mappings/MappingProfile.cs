using AutoMapper;
using CafeEmployeeManagement.Application.Cafes.Commands.CreateCafe;
using CafeEmployeeManagement.Application.Cafes.Commands.UpdateCafe;
using CafeEmployeeManagement.Application.Cafes.Queries.GetCafes;
using CafeEmployeeManagement.Application.Employees.Commands.CreateEmployee;
using CafeEmployeeManagement.Application.Employees.Commands.UpdateEmployee;
using CafeEmployeeManagement.Application.Employees.Queries.GetEmployees;
using CafeEmployeeManagement.Domain.Entities;

namespace CafeEmployeeManagement.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(x => x.DaysWorked, options =>
                {
                    options.MapFrom(e => (DateTime.UtcNow - e.CreatedDate).Days);
                }
            );
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();

            CreateMap<Cafe, CafeResponseDto>()
                .ForMember(x => x.Employees, options =>
                {
                    options.MapFrom(c => c.Employees.Count);
                });

            CreateMap<CreateCafeCommand, Cafe>();
            CreateMap<UpdateCafeCommand, Cafe>();
        }
    }
}

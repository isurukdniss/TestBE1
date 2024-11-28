using AutoMapper;
using CafeEmployeeManagement.Application.Common.Models;
using CafeEmployeeManagement.Application.Employees.Queries.GetEmployees;
using CafeEmployeeManagement.Domain.Entities;
using CafeEmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CafeEmployeeManagement.Application.Cafes.Queries.GetCafes
{
    public class GetCafeByIdQueryHandler : IRequestHandler<GetCafeByIdQuery, ApiResponse<CafeResponseDto>>
    {
        private readonly ICafeRepository cafeRepository;
        private readonly IMapper mapper;

        public GetCafeByIdQueryHandler(ICafeRepository cafeRepository, IMapper mapper)
        {
            this.cafeRepository = cafeRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CafeResponseDto>> Handle(GetCafeByIdQuery request, CancellationToken cancellationToken)
        {

            var cafe = await cafeRepository.GetByIdAsync(request.CafeId);

            if (cafe == null)
            {
                return ApiResponse<CafeResponseDto>.SetFailure(["Employee not found"]);
            }

            var employeeDto = mapper.Map<CafeResponseDto>(cafe);

            return ApiResponse<CafeResponseDto>.SetSuccess(employeeDto);
        }
    }
}

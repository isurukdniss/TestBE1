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
    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, ApiResponse<IEnumerable<CafeResponseDto>>>
    {
        private readonly ICafeRepository cafeRepository;
        private readonly IMapper mapper;

        public GetCafesQueryHandler(ICafeRepository cafeRepository, IMapper mapper)
        {
            this.cafeRepository = cafeRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CafeResponseDto>>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Cafe, bool>> filter = null;

            if (!string.IsNullOrEmpty(request.Location))
            {
                filter = x => x.Location == request.Location;
            }

            var cafeList = await cafeRepository.GetAllAsync(filter);

            var result = mapper.Map<IEnumerable<CafeResponseDto>>(cafeList);

            return ApiResponse<IEnumerable<CafeResponseDto>>.SetSuccess(result);

        }
    }
}

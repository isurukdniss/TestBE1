using AutoMapper;
using CafeEmployeeManagement.Application.Common.Models;
using CafeEmployeeManagement.Domain.Entities;
using CafeEmployeeManagement.Domain.Interfaces;
using MediatR;

namespace CafeEmployeeManagement.Application.Cafes.Commands.CreateCafe
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, ApiResponse<Guid>>
    {
        private readonly ICafeRepository cafeRepository;
        private readonly IMapper mapper;

        public CreateCafeCommandHandler(ICafeRepository cafeRepository, IMapper mapper)
        {
            this.cafeRepository = cafeRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<Guid>> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = mapper.Map<Cafe>(request);
            cafe.CreatedDate = DateTime.UtcNow;
            cafe.UpdatedDate = DateTime.UtcNow;

            await cafeRepository.AddAsync(cafe);

            return ApiResponse<Guid>.SetSuccess(cafe.Id);
        }
        
    }
}

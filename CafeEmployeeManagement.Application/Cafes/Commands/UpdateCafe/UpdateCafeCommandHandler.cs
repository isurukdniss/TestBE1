using AutoMapper;
using CafeEmployeeManagement.Application.Common.Models;
using CafeEmployeeManagement.Domain.Entities;
using CafeEmployeeManagement.Domain.Interfaces;
using MediatR;

namespace CafeEmployeeManagement.Application.Cafes.Commands.UpdateCafe
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, ApiResponse<bool>>
    {
        private readonly ICafeRepository cafeRepository;
        private readonly IMapper mapper;

        public UpdateCafeCommandHandler(ICafeRepository cafeRepository, IMapper mapper)
        {
            this.cafeRepository = cafeRepository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<bool>> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await cafeRepository.GetByIdAsync(request.Id);
            if (cafe == null)
            {
                return ApiResponse<bool>.SetFailure(["Employee not found"]);
            }

            cafe = mapper.Map<Cafe>(request);
            cafe.UpdatedDate = DateTime.UtcNow;

            await cafeRepository.UpdateAsync(cafe);

            return ApiResponse<bool>.SetSuccess(true);
        }
    }
}

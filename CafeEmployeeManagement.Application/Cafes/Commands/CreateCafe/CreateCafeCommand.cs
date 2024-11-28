using CafeEmployeeManagement.Application.Common.Models;
using MediatR;

namespace CafeEmployeeManagement.Application.Cafes.Commands.CreateCafe
{
    public class CreateCafeCommand : IRequest<ApiResponse<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Location { get; set; }
    }
}

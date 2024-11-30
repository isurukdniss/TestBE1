﻿using CafeEmployeeManagement.Application.Common.Models;
using MediatR;

namespace CafeEmployeeManagement.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<ApiResponse<bool>>
    {
        public string EmployeeId { get; set; }
    }
}


using ESApplication.Responses;
using ESDomain.AggregateModels.EmployeeDetailAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
 

namespace ESApplication.Commands.EmployeeDetails
{
    public class UpdateEmployeeCommand : IRequest<Response<string>>
    {
        public EmployeeDetail employeeDetail { get; set; } 
    }
}

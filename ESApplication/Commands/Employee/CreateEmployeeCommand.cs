using ESApplication.Responses;
using ESDomain.AggregateModels.EmployeeDetailAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ESApplication.Commands.EmployeeDetails
{
    public class CreateEmployeeCommand : IRequest<Response<string>>
    {
        public EmployeeDetail employeeDetail { get; set; }      
    }
}

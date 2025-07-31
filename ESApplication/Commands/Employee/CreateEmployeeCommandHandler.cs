using AutoMapper;
using ESApplication.Commands.EmployeeDetails;
using ESApplication.Responses;
using ESDomain.AggregateModels.EmployeeDetailAggregate; 
using ESDomain.IRepositories;
using MediatR; 
using System.Globalization;
using System.Net;

namespace ESApplication.Commands.ColdWorkPermitDetails
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<string>>
    {
        private readonly IEmployeeRepository _ColdWorkPermitRepository; 
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository ChildrenDetailsRepository,
            IMapper mapper)
        {
            _ColdWorkPermitRepository = ChildrenDetailsRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _ChildrenDetail = _mapper.Map<EmployeeDetail>(request);

                _ColdWorkPermitRepository.Add(_ChildrenDetail);
                await _ColdWorkPermitRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Employee Details Added Successfull";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Error";
            }
            return response;
        } 
    }
}
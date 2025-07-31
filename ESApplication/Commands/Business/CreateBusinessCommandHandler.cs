using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;
using ESInfrastructure.Repository;

namespace ESApplication.Commands.Business
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, Response<string>>
    {
        private readonly IBusinessRepository _BusinessRepository; 
        private readonly IMapper _mapper;

        public CreateBusinessCommandHandler(IBusinessRepository BusinessRepository,
            IMapper mapper)
        {
            _BusinessRepository = BusinessRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _userDetail = _mapper.Map<BusinessDetails>(request);

                _BusinessRepository.Add(_userDetail);
                await _BusinessRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Business Details Added Successfully";
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
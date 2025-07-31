using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;
using ESInfrastructure.Repository;
using ESDomain.AggregateModels.BusinessHoursAggregate;

namespace ESApplication.Commands.BusinessHours
{
    public class CreateBusinessHoursCommandHandler : IRequestHandler<CreateBusinessHoursCommand, Response<string>>
    {
        private readonly IBusinessHoursRepository _BusinessHoursRepository; 
        private readonly IMapper _mapper;

        public CreateBusinessHoursCommandHandler(IBusinessHoursRepository BusinessHoursRepository,
            IMapper mapper)
        {
            _BusinessHoursRepository = BusinessHoursRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateBusinessHoursCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _userDetail = _mapper.Map<BusinessHoursDetails>(request);

                _BusinessHoursRepository.Add(_userDetail);
                await _BusinessHoursRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Business Hours Details Added Successfully";
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
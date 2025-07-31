using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.BusinessHours
{
    public class DeleteBusinessHoursCommandHandler : IRequestHandler<DeleteBusinessHoursCommand, Response<string>>
    {
        private readonly IBusinessHoursRepository _BusinessHoursRepository;
        private readonly IMapper _mapper;
        public DeleteBusinessHoursCommandHandler(IBusinessHoursRepository BusinessHoursRepository,
            IMapper mapper)
        {
            _BusinessHoursRepository = BusinessHoursRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteBusinessHoursCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<BusinessHoursDetails>(request);
                this._BusinessHoursRepository.Delete(_request);
                await this._BusinessHoursRepository.UnitOfWork.DeleteRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Business Hours Details Deleted Successfully"
                };
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

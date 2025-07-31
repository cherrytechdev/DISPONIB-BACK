using AutoMapper;
using ESApplication.Commands.Business;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;
using ESApplication.Commands.BusinessHours;
using ESDomain.AggregateModels.BusinessHoursAggregate;

namespace ESApplication.Commands.BusinessHours
{
    public class UpdateBusinessHoursCommandHandler : IRequestHandler<UpdateBusinessHoursCommand, Response<string>>
    {
        private readonly IBusinessHoursRepository _BusinessHoursRepository;
        private readonly IMapper _mapper;
        public UpdateBusinessHoursCommandHandler(IBusinessHoursRepository BusinessHoursRepository,
            IMapper mapper)
        {
            _BusinessHoursRepository = BusinessHoursRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateBusinessHoursCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<BusinessHoursDetails>(request);
                this._BusinessHoursRepository.Update(_request);
                await this._BusinessHoursRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Business Hours Details Edited Successfully"
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

using AutoMapper;
using ESApplication.Commands.Business;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.Business
{
    public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, Response<string>>
    {
        private readonly IBusinessRepository _BusinessRepository;
        private readonly IMapper _mapper;
        public UpdateBusinessCommandHandler(IBusinessRepository BusinessRepository,
            IMapper mapper)
        {
            _BusinessRepository = BusinessRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<BusinessDetails>(request);
                this._BusinessRepository.Update(_request);
                await this._BusinessRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Business Details Edited Successfully"
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

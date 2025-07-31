using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.ColdWorkPermitDetailAggregate;
using ESDomain.AggregateModels.RequesterActionAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.UpdateRequesterActionDetails
{
    public class UpdateRequesterActionCommandHandler : IRequestHandler<UpdateRequesterActionCommand, Response<string>>
    {
        private readonly IRequesterActionRepository _requesterActionRepository;
        private readonly IMapper _mapper;
        public UpdateRequesterActionCommandHandler(IRequesterActionRepository requesterActionRepository,
            IMapper mapper)
        {
            _requesterActionRepository = requesterActionRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateRequesterActionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<UpdateRequesterActionDetail>(request);
                this._requesterActionRepository.Update(_request);
                await this._requesterActionRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Requester Action Details Edited Successfully"
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

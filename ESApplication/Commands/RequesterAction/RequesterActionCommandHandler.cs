using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.ColdWorkPermitDetailAggregate;
using ESDomain.AggregateModels.RequesterActionAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.UpdateRequesterAction
{
    public class RequesterActionCommandHandler : IRequestHandler<RequesterActionCommand, Response<string>>
    {
        private readonly IRequesterActionRepository _requesterActionRepository;
        private readonly IMapper _mapper;
        public RequesterActionCommandHandler(IRequesterActionRepository requesterActionRepository,
            IMapper mapper)
        {
            _requesterActionRepository = requesterActionRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(RequesterActionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<RequesterActionDetail>(request);
                this._requesterActionRepository.Add(_request);
                await this._requesterActionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "RequesterAction Details Saved Successfully"
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

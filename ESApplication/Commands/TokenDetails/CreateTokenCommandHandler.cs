using AutoMapper;
using ESApplication.Responses;
using ESDomain.IRepositories;
using ESDomain.AggregateModels.TokenDetailsAggregate;
using System.Net;
using ESApplication.Commands.Business;
using MediatR;

namespace ESApplication.Commands.TokenDetails
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Response<string>>
    {
        private readonly ITokenDetailsRepository _tokenDetailsRepository;
        private readonly IMapper _mapper;

        public CreateTokenCommandHandler(ITokenDetailsRepository tokenDetailsRepository,
            IMapper mapper)
        {
            _tokenDetailsRepository = tokenDetailsRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _tokenDetail = _mapper.Map<TokenDetail>(request);

                _tokenDetailsRepository.Add(_tokenDetail);
                await _tokenDetailsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Token Details Added Successfull";
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


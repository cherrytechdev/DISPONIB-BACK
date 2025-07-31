using AutoMapper;
using ESApplication.Commands.UserDetails;
using ESApplication.Responses;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using MediatR; 
using System.Net;

namespace ESApplication.Commands.UserDetails
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, Response<string>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;
        public UpdateUserDetailsCommandHandler(IUserDetailsRepository userDetailsRepository,
            IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<UserDetail>(request);
                this._userDetailsRepository.Update(_request);
                await this._userDetailsRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "User Details Edited Successfully"
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

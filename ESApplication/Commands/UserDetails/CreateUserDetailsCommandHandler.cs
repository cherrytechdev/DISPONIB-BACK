using AutoMapper;
using ESApplication.Commands.UserDetails;
using ESApplication.Responses;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using MediatR; 
using System.Globalization;
using System.Net;

namespace ESApplication.Commands.UserDetails
{
    public class CreateUserDetailsCommandHandler : IRequestHandler<CreateUserDetailsCommand, Response<string>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository; 
        private readonly IMapper _mapper;

        public CreateUserDetailsCommandHandler(IUserDetailsRepository userDetailsRepository,
            IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _userDetail = _mapper.Map<UserDetail>(request);

                _userDetailsRepository.Add(_userDetail);
                await _userDetailsRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "User Details Added Successfull";
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
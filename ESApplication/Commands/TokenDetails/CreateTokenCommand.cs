using ESApplication.Responses;
using MediatR; 
namespace ESApplication.Commands.TokenDetails
{
    public class CreateTokenCommand : IRequest<Response<string>>
    {
        public string token { get; set; }

    }
}

using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.Business
{
    public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, Response<string>>
    {
        private readonly IBusinessRepository _BusinessRepository;
        private readonly IMapper _mapper;
        public DeleteBusinessCommandHandler(IBusinessRepository BusinessRepository,
            IMapper mapper)
        {
            _BusinessRepository = BusinessRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<BusinessDetails>(request);
                this._BusinessRepository.Delete(_request);
                await this._BusinessRepository.UnitOfWork.DeleteRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Business Details Deleted Successfully"
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

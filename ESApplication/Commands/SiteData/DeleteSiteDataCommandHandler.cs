using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.SiteData
{
    public class DeleteSiteDataCommandHandler : IRequestHandler<DeleteSiteDataCommand, Response<string>>
    {
        private readonly ISiteDataRepository _SiteDataRepository;
        private readonly IMapper _mapper;
        public DeleteSiteDataCommandHandler(ISiteDataRepository SiteDataRepository,
            IMapper mapper)
        {
            _SiteDataRepository = SiteDataRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteSiteDataCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<SiteDataDetails>(request);
                this._SiteDataRepository.Delete(_request);
                await this._SiteDataRepository.UnitOfWork.DeleteRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "SiteData Details Deleted Successfully"
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

using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.SiteData
{
    public class UpdateSiteDataCommandHandler : IRequestHandler<UpdateSiteDataCommand, Response<string>>
    {
        private readonly ISiteDataRepository _SiteDataRepository;
        private readonly IMapper _mapper;
        public UpdateSiteDataCommandHandler(ISiteDataRepository SiteDataRepository,
            IMapper mapper)
        {
            _SiteDataRepository = SiteDataRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateSiteDataCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<SiteDataDetails>(request);
                this._SiteDataRepository.Update(_request);
                await this._SiteDataRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "SiteData Details Edited Successfully"
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

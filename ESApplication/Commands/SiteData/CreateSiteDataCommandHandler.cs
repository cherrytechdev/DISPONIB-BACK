using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;


namespace ESApplication.Commands.SiteData
{
    public class CreateSiteDataCommandHandler : IRequestHandler<CreateSiteDataCommand, Response<string>>
    {
        private readonly ISiteDataRepository _SiteDataRepository;
        private readonly IMapper _mapper;

        public CreateSiteDataCommandHandler(ISiteDataRepository SiteDataRepository,
            IMapper mapper)
        {
            _SiteDataRepository = SiteDataRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSiteDataCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                if (request.fileNames != null)
                {
                    foreach (var imageName in request.fileNames)
                    {
                        request.filename = imageName;
                        var _request = _mapper.Map<SiteDataDetails>(request);
                        this._SiteDataRepository.Upload(_request);
                        await this._SiteDataRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    }
                }
                else
                {
                    var _request = _mapper.Map<SiteDataDetails>(request);
                    this._SiteDataRepository.Upload(_request);
                    await this._SiteDataRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                }

                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Site Images Uploaded Successfully"
                };


                //var _userDetail = _mapper.Map<SiteDataDetails>(request);

                //_SiteDataRepository.Add(_userDetail);
                //await _SiteDataRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                //response.Succeeded = true;
                //response.StatusCode = (int)HttpStatusCode.Created;
                //response.Message = "SiteData Details Added Successfull";
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
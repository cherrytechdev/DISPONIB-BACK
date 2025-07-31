using AutoMapper;
using ESApplication.Commands.Business;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;
using ESDomain.AggregateModels.BusinessImageAggregate;
using Org.BouncyCastle.Asn1.Ocsp;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace ESApplication.Commands.BusinessImages
{
    public class UploadBusinessImagesCommandHandler : IRequestHandler<UploadBusinessImagesCommand, Response<string>>
    {
        private readonly IBusinessImageRepository _BusinessImageRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UploadBusinessImagesCommandHandler(IBusinessImageRepository BusinessImageRepository,
            IMapper mapper, IConfiguration config)
        {
            _BusinessImageRepository = BusinessImageRepository;
            _mapper = mapper;
            _config = config;
        }
        public async Task<Response<string>> Handle(UploadBusinessImagesCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            int fileCount = 0;

            try
            {                
                foreach (var imageName in request.ImageNames)
                {   
                    request.ImageName = imageName; 
                    var _request = _mapper.Map<BusinessImageDetails>(request);
                    this._BusinessImageRepository.Upload(_request);
                    await this._BusinessImageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    
                }
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Business Images Uploaded Successfully"
                };
                
            }
            catch(Exception ex)
            {

                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Error";
                return response;
            }
            
        }
    }
}

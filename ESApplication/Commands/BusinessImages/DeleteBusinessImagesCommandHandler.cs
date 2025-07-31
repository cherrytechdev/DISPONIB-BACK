using AutoMapper;
using ESApplication.Commands.BusinessHours;
using ESApplication.Responses;
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.AggregateModels.BusinessImageAggregate;
using ESDomain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Commands.BusinessImages
{
    internal class DeleteBusinessImagesCommandHandler : IRequestHandler<DeleteBusinessImagesCommand, Response<string>>
    {
        private readonly IBusinessImageRepository _BusinessImageRepository;
        private readonly IMapper _mapper;
        public DeleteBusinessImagesCommandHandler(IBusinessImageRepository BusinessImageRepository,
            IMapper mapper)
        {
            _BusinessImageRepository = BusinessImageRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteBusinessImagesCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {               

                if (!System.IO.File.Exists(request.ImagePath))
                {
                    response.Succeeded = false;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Business Image not Found";
                }

                System.IO.File.Delete(request.ImagePath);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Error";
            }

            if (System.IO.File.Exists(request.ImagePath))
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Error";

                return response;
            }

            try
            {
                var _request = _mapper.Map<BusinessImageDetails>(request);
                this._BusinessImageRepository.Delete(_request);
                await this._BusinessImageRepository.UnitOfWork.DeleteRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Business Image Deleted Successfully"
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

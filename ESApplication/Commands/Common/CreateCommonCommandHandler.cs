using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.CommonAggregate;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;
 

namespace ESApplication.Commands.CommonData
{
    public class CreateCommonCommandHandler : IRequestHandler<CreateCommonCommand, Response<string>>
    {
        private readonly ICommonRepository _CommonRepository; 
        private readonly IMapper _mapper;

        public CreateCommonCommandHandler(ICommonRepository CommonRepository,
            IMapper mapper)
        {
            _CommonRepository = CommonRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCommonCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _CommonDetail = _mapper.Map<CommonDetails>(request);

                _CommonRepository.Add(_CommonDetail);
                await _CommonRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Common Details Added Successfull";
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
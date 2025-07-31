using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.Promotion
{
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, Response<string>>
    {
        private readonly IPromotionRepository _PromotionRepository; 
        private readonly IMapper _mapper;

        public CreatePromotionCommandHandler(IPromotionRepository PromotionRepository,
            IMapper mapper)
        {
            _PromotionRepository = PromotionRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _userDetail = _mapper.Map<PromotionDetails>(request);

                _PromotionRepository.Add(_userDetail);
                await _PromotionRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Promotion Details Added Successfully";
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
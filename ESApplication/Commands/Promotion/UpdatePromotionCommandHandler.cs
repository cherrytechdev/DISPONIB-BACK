using AutoMapper;
using ESApplication.Commands.Promotion;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.Category
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, Response<string>>
    {
        private readonly IPromotionRepository _PromotionRepository;
        private readonly IMapper _mapper;
        public UpdatePromotionCommandHandler(IPromotionRepository PromotionRepository,
            IMapper mapper)
        {
            _PromotionRepository = PromotionRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<PromotionDetails>(request);
                this._PromotionRepository.Update(_request);
                await this._PromotionRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Promotion Details Edited Successfully"
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

using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.Promotion
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand, Response<string>>
    {
        private readonly IPromotionRepository _PromotionRepository;
        private readonly IMapper _mapper;
        public DeletePromotionCommandHandler(IPromotionRepository PromotionRepository,
            IMapper mapper)
        {
            _PromotionRepository = PromotionRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<PromotionDetails>(request);
                this._PromotionRepository.Delete(_request);
                await this._PromotionRepository.UnitOfWork.DeleteRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Promotion Details Deleted Successfully"
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

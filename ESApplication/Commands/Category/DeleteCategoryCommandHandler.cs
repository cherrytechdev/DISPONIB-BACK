using AutoMapper;
using ESApplication.Commands.UserDetails;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using MediatR; 
using System.Net;

namespace ESApplication.Commands.Category
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<string>>
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;
        public DeleteCategoryCommandHandler(ICategoryRepository CategoryRepository,
            IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var _request = _mapper.Map<CategoryDetails>(request);
                this._CategoryRepository.Delete(_request);
                await this._CategoryRepository.UnitOfWork.DeleteRecordAsync(cancellationToken);
                return new Response<string>()
                {
                    Succeeded = true,
                    Message = "Category Details Deleted Successfully"
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

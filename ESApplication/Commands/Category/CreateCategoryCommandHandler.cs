using AutoMapper;
using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.IRepositories;
using MediatR;
using System.Net;

namespace ESApplication.Commands.Category
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<string>>
    {
        private readonly ICategoryRepository _CategoryRepository; 
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository CategoryRepository,
            IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;            
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            { 
                var _userDetail = _mapper.Map<CategoryDetails>(request);

                _CategoryRepository.Add(_userDetail);
                await _CategoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Category Details Added Successfully";
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
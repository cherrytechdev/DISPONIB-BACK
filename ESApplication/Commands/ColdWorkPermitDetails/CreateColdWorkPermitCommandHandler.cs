//using AutoMapper;
//using ESApplication.Responses;
//using ESDomain.AggregateModels.ColdWorkPermitDetailAggregate; 
//using ESDomain.IRepositories;
//using MediatR; 
//using System.Globalization;
//using System.Net;

//namespace ESApplication.Commands.ColdWorkPermitDetails
//{
//    public class CreateColdWorkPermitCommandHandler : IRequestHandler<CreateColdWorkPermitCommand, Response<string>>
//    {
//        private readonly IColdWorkPermitRepository _ColdWorkPermitRepository; 
//        private readonly IMapper _mapper;

//        public CreateColdWorkPermitCommandHandler(IColdWorkPermitRepository ChildrenDetailsRepository,
//            IMapper mapper)
//        {
//            _ColdWorkPermitRepository = ChildrenDetailsRepository;            
//            _mapper = mapper;
//        }

//        public async Task<Response<string>> Handle(CreateColdWorkPermitCommand request, CancellationToken cancellationToken)
//        {
//            var response = new Response<string>();
//            try
//            { 
//                var _ChildrenDetail = _mapper.Map<EmployeeDetail>(request);

//                _ColdWorkPermitRepository.Add(_ChildrenDetail);
//                await _ColdWorkPermitRepository.UnitOfWork.SaveChangesAsync(cancellationToken); 
//                response.Succeeded = true;
//                response.StatusCode = (int)HttpStatusCode.Created;
//                response.Message = "ColdWorkPermit Details Added Successfull";
//            }
//            catch (Exception ex)
//            {
//                response.Succeeded = false;
//                response.StatusCode = (int)HttpStatusCode.BadRequest;
//                response.Message = "Error";
//            }
//            return response;
//        } 
//    }
//}
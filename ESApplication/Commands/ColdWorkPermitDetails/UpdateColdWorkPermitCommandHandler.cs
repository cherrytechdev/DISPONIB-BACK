//using AutoMapper;
//using ESApplication.Responses;
//using ESDomain.AggregateModels.ColdWorkPermitDetailAggregate;
//using ESDomain.AggregateModels.UserDetailsAggregate;
//using ESDomain.IRepositories;
//using MediatR;
//using System.Net;

//namespace ESApplication.Commands.ColdWorkPermitDetails
//{
//    public class UpdateColdWorkPermitCommandHandler : IRequestHandler<UpdateColdWorkPermitCommand, Response<string>>
//    {
//        private readonly IColdWorkPermitRepository _ColdWorkPermitRepository;
//        private readonly IMapper _mapper;
//        public UpdateColdWorkPermitCommandHandler(IColdWorkPermitRepository coldWorkPermitRepository,
//            IMapper mapper)
//        {
//            _ColdWorkPermitRepository = coldWorkPermitRepository;
//            _mapper = mapper;
//        }
//        public async Task<Response<string>> Handle(UpdateColdWorkPermitCommand request, CancellationToken cancellationToken)
//        {
//            var response = new Response<string>();
//            try
//            {
//                var _request = _mapper.Map<EmployeeDetail>(request);
//                this._ColdWorkPermitRepository.Update(_request);
//                await this._ColdWorkPermitRepository.UnitOfWork.UpdateRecordAsync(cancellationToken);
//                return new Response<string>()
//                {
//                    Succeeded = true,
//                    Message = "ColdWorkPermit Details Edited Successfully"
//                };
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

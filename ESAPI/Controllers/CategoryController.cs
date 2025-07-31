using ESApplication.AggregateModels;
using ESApplication.Commands.Category;
using ESApplication.Models;
using ESApplication.Queries.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public CategoryController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config; 

        }

        #region Category Data  

        /// <summary>
        /// To get Category details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{userid}/{code}")]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoryDetails(string userid,string code)
        {
            var res = await _mediator.Send(new GetCategoryDetailsQuery() { userid = userid,code=code });
            if (res != null)
            { 
                return Ok(res);
            }
            else
            {
                return Problem("Records not found! Please contact the administrator.", null, StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// To create Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        /// <summary>
        /// To update Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }


        /// <summary>
        /// To update Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("deletecategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion 
    }  
}

using Microsoft.AspNetCore.Authorization;
using SurveyBaskt.Errors;

namespace SurveyBaskt.Controllers
{
    [Route("api/Polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuesionsController(IQuesionService _quesionService) : ControllerBase
    {

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromRoute] int pollId , CancellationToken cancellationToken=default)
        {
            var result = await _quesionService.GetAllAsync(pollId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return result.ToProblem(StatusCodes.Status404NotFound);    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int pollId, [FromRoute] int id,CancellationToken cancellationToken=default   )
        {
            var result = await _quesionService.GetAsync(pollId, id ,cancellationToken);
            
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem(StatusCodes.Status404NotFound); 

        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromRoute]int pollId ,[FromBody] QuesionRequest request , CancellationToken cancellationToken=default)
        {
            var result =await _quesionService.AddAsync(pollId , request , cancellationToken);
            if (result.IsSuccess) return  CreatedAtAction(nameof(GetById) , new {pollId = pollId , id = result.Value.Id },result.Value);
            return result.Error.Equals(QuesionError.DubliateContent) ? result.ToProblem(StatusCodes.Status409Conflict) 
                                                                        : result.ToProblem(StatusCodes.Status404NotFound);
        }

    }
}

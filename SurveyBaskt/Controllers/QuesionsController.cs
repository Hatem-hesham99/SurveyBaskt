using Microsoft.AspNetCore.Authorization;
using SurveyBaskt.Errors;

namespace SurveyBaskt.Controllers
{
    [Route("api/Polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuesionsController(IQuesionService _quesionService) : ControllerBase
    {

        [HttpPost("")]
        public async Task<IActionResult> Add([FromRoute]int pollId ,[FromBody] QuesionRequest request , CancellationToken cancellationToken=default)
        {
            var result =await _quesionService.AddAsync(pollId , request , cancellationToken);

            if (result.IsSuccess) return Ok();

            return result.Error.Equals(QuesionError.DubliateContent) ? result.ToProblem(StatusCodes.Status409Conflict) : result.ToProblem(StatusCodes.Status404NotFound);

        }
    }
}

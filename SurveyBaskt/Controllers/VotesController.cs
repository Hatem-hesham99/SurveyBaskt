using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBaskt.Errors;
using SurveyBaskt.Extentions;
using System.Security.Claims;

namespace SurveyBaskt.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class VotesController(IQuesionService _quesionservice) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Start([FromRoute] int pollId,CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

         

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _quesionservice.GetCurrentlyActiveQuesionsAsync(pollId, userId, cancellationToken);

            if (result.IsSuccess) return Ok(result.Value);

            return result.Error.Equals(PollError.PollNotFound) ? result.ToProblem(StatusCodes.Status404NotFound) : result.ToProblem(StatusCodes.Status409Conflict);

        } 

    }
}

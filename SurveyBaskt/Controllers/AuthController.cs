using Microsoft.AspNetCore.Authorization;

namespace SurveyBaskt.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService ) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request,CancellationToken cancellationToken)
        {
            var response = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            return response.IsSuccess ?
                Ok(response.Value) : ResultExtention.ToProblem(response, StatusCodes.Status400BadRequest);
               
        }

       

    }
}

using Microsoft.AspNetCore.Diagnostics;

namespace SurveyBaskt.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
          _logger.LogError(exception, "An unhandled exception occurred at  . {massage}",exception.Message  );
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred. from  GlobalExceptionHandler ",
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
           await httpContext.Response.WriteAsJsonAsync( problemDetails ,cancellationToken: cancellationToken);

            return true;

        }
    }
}

namespace SurveyBaskt.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next , ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
              await  _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred at  . {massage}", context.GetEndpoint() );

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred.",
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",

                };

                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //context.Response.ContentType = "application/json";
               // var errorResponse = new { message = $"An unexpected error occurred. {context.GetEndpoint()}" };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}

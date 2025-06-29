using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                throw exception;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Argument null exception: {Message}", ex.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Missing argument"
                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex, "Bad request null exception: {Message}", ex.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad request",
                    Detail = ex.Message
                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }

            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Invalid operation: {Message}", ex.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Notfound",
                    Detail = ex.Message

                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server error"
                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }

            return true;
        }

    }
}

using ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                await HandleNotFoundResponseAsync(httpContext);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Something Went Wrong");

                var Response = new ProblemDetails
                {
                    Title = "An unexpected error occurred!",
                    Detail = Ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = Ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    }
                };
                httpContext.Response.StatusCode = Response.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }

        private static async Task HandleNotFoundResponseAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound && !httpContext.Response.HasStarted)
            {
                var problemDetailsNotFound = new ProblemDetails
                {
                    Title = "Resource not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = "The requested resource was not found",
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetailsNotFound);
            }
        }
    }
}

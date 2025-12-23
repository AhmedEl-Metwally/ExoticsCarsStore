using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Something Went Wrong");

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var problemDetails = new ProblemDetails 
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred!",
                    Detail = Ex.Message,
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}

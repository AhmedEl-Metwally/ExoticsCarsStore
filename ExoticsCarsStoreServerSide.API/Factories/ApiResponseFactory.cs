using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateAPiValidationResponse(ActionContext actionContext)
        {
            var Errors = actionContext.ModelState
                .Where(E => E.Value!.Errors.Count > 0)
                .ToDictionary(Key => Key.Key, Value => Value.Value!.Errors.Select(E => E.ErrorMessage).ToArray());

            var Response = new ValidationProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Detail = "See the errors property for more details.",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "errors", Errors } }
            };
            return new BadRequestObjectResult(Response);
        }
    }
}

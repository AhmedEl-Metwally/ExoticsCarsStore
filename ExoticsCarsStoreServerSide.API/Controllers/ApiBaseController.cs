using ExoticsCarsStoreServerSide.Shared.CommonResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExoticsCarsStoreServerSide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        //Handle Result Without Value

        protected IActionResult HandleResult(ErrorToReturn returnValue)
        {
            if (returnValue.IsSuccess)
                return NoContent();
            else
                return HandleProblem(returnValue.Errors);
        }



        //Handle Result With Value
        protected ActionResult<TValue> HandleResult<TValue>(ErrorToReturnValue<TValue> returnValue)
        {
            if (returnValue.IsSuccess)
                return Ok(returnValue.Value);
            else
                return HandleProblem(returnValue.Errors);
        }

        // Helper methods 
        private ActionResult HandleProblem(IReadOnlyList<ValidationErrorToReturn> errors)
        {
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Error Occurred");

            if (errors.All(E => E.ErrorType == ErrorType.ValidationError))
                return HandleValidationProblem(errors);
            return HandleSingleErrorProblem(errors[0]);
        }

        private ActionResult HandleSingleErrorProblem(ValidationErrorToReturn error)
            => Problem
            (
                title: error.StatusCode,
                detail: error.ErrorDescription,
                type: error.ErrorType.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.ErrorType)
            );

        private static int MapErrorTypeToStatusCode(ErrorType errorType)
            => errorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.ValidationError => StatusCodes.Status400BadRequest,
                ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

        private ActionResult HandleValidationProblem(IReadOnlyList<ValidationErrorToReturn> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
                modelState.AddModelError(error.StatusCode, error.ErrorDescription);
            return ValidationProblem(modelState);

        }

    }
}

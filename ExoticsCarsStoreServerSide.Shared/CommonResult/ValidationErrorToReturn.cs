namespace ExoticsCarsStoreServerSide.Shared.CommonResult
{
    public class ValidationErrorToReturn
    {
        public string StatusCode { get; set; } 
        public string ErrorDescription { get; set; } 
        public ErrorType ErrorType  { get; set; } 

        private ValidationErrorToReturn(string statusCode, string errorDescription, ErrorType errorType)
        {
            StatusCode = statusCode;
            ErrorDescription = errorDescription;
            ErrorType = errorType;
        }

        public static ValidationErrorToReturn Failure(string StatusCode = "General.Failure", string ErrorDescription = "A general Failure Has Occurred")
            => new(StatusCode, ErrorDescription, ErrorType.Failure);

        public static ValidationErrorToReturn ValidationError(string StatusCode = "General.ValidationError", string ErrorDescription = "Validation Error Has Occurred")
            => new(StatusCode, ErrorDescription, ErrorType.ValidationError);

        public static ValidationErrorToReturn NotFound(string StatusCode = "General.NotFound", string ErrorDescription = "The Requested Resource was not found")
            => new(StatusCode, ErrorDescription, ErrorType.NotFound);

        public static ValidationErrorToReturn Unauthorized(string StatusCode = "General.UnAuthorized", string ErrorDescription = "You are Not Authorized to perform this action")
            => new(StatusCode, ErrorDescription, ErrorType.Unauthorized);

        public static ValidationErrorToReturn Forbidden(string StatusCode = "General.Forbidden", string ErrorDescription = "You don't have the access to this resource,Access denied")
            => new(StatusCode, ErrorDescription, ErrorType.Forbidden);

        public static ValidationErrorToReturn InvalidCredentials(string StatusCode = "General.InvalidCredentials", string ErrorDescription = "Your Credentials is not valid to reach this resource")
            => new(StatusCode, ErrorDescription, ErrorType.InvalidCredentials);

    }
}

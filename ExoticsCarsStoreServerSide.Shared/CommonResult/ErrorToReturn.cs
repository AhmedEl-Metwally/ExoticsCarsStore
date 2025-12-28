namespace ExoticsCarsStoreServerSide.Shared.CommonResult
{
    public class ErrorToReturn
    {
        protected readonly List<ValidationErrorToReturn> _validationErrorToReturn = [];
        public bool IsSuccess => _validationErrorToReturn.Count == 0;
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<ValidationErrorToReturn> Errors => _validationErrorToReturn;

        // Ok
        protected ErrorToReturn(){}

        // Fail With Errors
        protected ErrorToReturn(ValidationErrorToReturn validationErrorToReturn) => _validationErrorToReturn.Add(validationErrorToReturn);

        // Fail With Multiple Errors
        protected ErrorToReturn(List<ValidationErrorToReturn> validationErrorToReturns) => _validationErrorToReturn.AddRange(validationErrorToReturns);

        public static ErrorToReturn Ok() => new();
        public static ErrorToReturn Fail(ValidationErrorToReturn validationErrorToReturn) => new(validationErrorToReturn);
        public static ErrorToReturn Fail(List<ValidationErrorToReturn> validationErrorToReturns) => new(validationErrorToReturns);

    }
}

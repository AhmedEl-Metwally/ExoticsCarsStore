namespace ExoticsCarsStoreServerSide.Shared.CommonResult
{
    public class ErrorToReturnValue<TValue> : ErrorToReturn
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        // Ok with Value
        private ErrorToReturnValue(TValue value) : base() => _value = value;
        // Fail With Errors
        private ErrorToReturnValue(ValidationErrorToReturn validationErrorToReturn) : base(validationErrorToReturn) => _value = default!;
        // Fail With Multiple Errors
        private ErrorToReturnValue(List<ValidationErrorToReturn> validationErrorToReturns) : base(validationErrorToReturns) => _value = default!;

        public static ErrorToReturnValue<TValue> Ok(TValue value) => new(value);
        public static new ErrorToReturnValue<TValue> Fail(ValidationErrorToReturn validationErrorToReturn) => new(validationErrorToReturn);
        public static new ErrorToReturnValue<TValue> Fail(List<ValidationErrorToReturn> validationErrorToReturns) => new(validationErrorToReturns);

        public static implicit operator ErrorToReturnValue<TValue>(TValue value) => Ok(value);
        public static implicit operator ErrorToReturnValue<TValue>(ValidationErrorToReturn validationErrorToReturn) => Fail(validationErrorToReturn);
        public static implicit operator ErrorToReturnValue<TValue>(List<ValidationErrorToReturn> validationErrorToReturns) => Fail(validationErrorToReturns);
    }
}

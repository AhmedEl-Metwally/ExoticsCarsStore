namespace ExoticsCarsStoreServerSide.Shared.CommonResult
{
    public enum ErrorType
    {
        Failure = 0,
        ValidationError = 1,
        NotFound = 2,
        Unauthorized = 3,
        Forbidden = 4,
        InvalidCredentials = 5,
    }
}
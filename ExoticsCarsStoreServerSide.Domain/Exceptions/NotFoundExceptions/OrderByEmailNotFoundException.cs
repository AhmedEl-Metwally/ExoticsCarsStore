namespace ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions
{
    public sealed class OrderByEmailNotFoundException(string Email) : NotFoundException($"No orders found for user with email: {Email}")
    {
    }
}

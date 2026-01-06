namespace ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions
{
    public sealed class OrderByIdNotFoundException(Guid orderId) : NotFoundException($"No order found with Id: {orderId}")
    {
    }
}

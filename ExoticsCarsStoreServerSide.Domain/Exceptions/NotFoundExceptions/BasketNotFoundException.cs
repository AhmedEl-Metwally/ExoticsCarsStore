namespace ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions
{
    public sealed class BasketNotFoundException(string id ) : NotFoundException($"Basket With id {id} is not Found")
    {
    }
}

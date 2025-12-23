namespace ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions
{
    public sealed class ProductNotFoundException(int id) : NotFoundException($"Product With id {id} is not Found")
    {
    }
}

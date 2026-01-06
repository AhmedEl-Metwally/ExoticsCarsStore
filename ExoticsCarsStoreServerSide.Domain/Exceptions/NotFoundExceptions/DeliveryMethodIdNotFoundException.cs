namespace ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions
{
    public sealed class DeliveryMethodIdNotFoundException(int id) : NotFoundException($"Can not Find Delivery Method With Id  = {id}")
    {
    }
}

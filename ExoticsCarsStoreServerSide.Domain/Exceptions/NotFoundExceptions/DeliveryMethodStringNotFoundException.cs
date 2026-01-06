namespace ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions
{
    public sealed class DeliveryMethodStringNotFoundException(string deliveryMethod) : NotFoundException($"Delivery Method With Name {deliveryMethod} is not Found")
    {
    }
}

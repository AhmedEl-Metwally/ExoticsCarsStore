namespace ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS
{
    public record OrderDTO
    {
        public string BasketId { get; init; } = default!;
        public int DeliveryMethodId { get; init; }
        public AddressDTO Address { get; init; } = default!;
    }
}

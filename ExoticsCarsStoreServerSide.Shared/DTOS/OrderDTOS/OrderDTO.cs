namespace ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS
{
    public class OrderDTO
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public AddressDTO Address { get; set; } = default!;
    }
}

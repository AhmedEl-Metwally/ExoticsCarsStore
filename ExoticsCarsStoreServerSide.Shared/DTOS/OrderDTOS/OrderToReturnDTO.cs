namespace ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS
{
    public class OrderToReturnDTO
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public ICollection<OrderItemDTO> Items { get; set; } = [];
        public AddressDTO Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
       // public decimal DeliveryCost { get; set; }
    }
}

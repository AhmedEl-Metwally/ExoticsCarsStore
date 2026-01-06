namespace ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS
{
    public record OrderToReturnDTO
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;
        public ICollection<OrderItemDTO> Items { get; init; } = [];
        public AddressDTO Address { get; init; } = default!;
        public string DeliveryMethod { get; init; } = string.Empty;
        public string OrderStatus { get; init; } = string.Empty;
        public DateTimeOffset OrderDate { get; init; }
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
        // public decimal DeliveryCost { get; set; }
    }
}

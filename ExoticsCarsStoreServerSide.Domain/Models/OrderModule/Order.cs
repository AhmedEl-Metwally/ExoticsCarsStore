namespace ExoticsCarsStoreServerSide.Domain.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress Address { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }

    }
}

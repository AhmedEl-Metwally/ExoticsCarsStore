namespace ExoticsCarsStoreServerSide.Domain.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? ClientSecret { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}

namespace ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS
{
    public class BasketDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? ClientSecret { get; set; }
        public ICollection<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();

    }

}

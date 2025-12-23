namespace ExoticsCarsStoreServerSide.Domain.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}

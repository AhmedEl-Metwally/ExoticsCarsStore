namespace ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS
{
    public record BasketDTO
    {
        public string Id { get; init; } = string.Empty;
        public ICollection<BasketItemDTO> Items { get; init; } = new List<BasketItemDTO>();

    }

}

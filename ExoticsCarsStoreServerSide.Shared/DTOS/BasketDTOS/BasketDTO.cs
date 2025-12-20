namespace ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS
{
    public record BasketDTO(string Id, ICollection<BasketItemDTO> Items);

}

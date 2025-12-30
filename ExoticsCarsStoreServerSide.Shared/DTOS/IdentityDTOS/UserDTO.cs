using System.ComponentModel.DataAnnotations;

namespace ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS
{
    public record UserDTO
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string DisplayName { get; init; } = string.Empty;
        public string Token { get; init; } = string.Empty;
    }
}

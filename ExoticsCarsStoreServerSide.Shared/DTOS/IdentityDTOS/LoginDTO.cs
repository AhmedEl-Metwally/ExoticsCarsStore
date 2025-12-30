using System.ComponentModel.DataAnnotations;

namespace ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS
{
    public record LoginDTO
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}

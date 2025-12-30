using System.ComponentModel.DataAnnotations;

namespace ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS
{
    public record RegisterDTO
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string DisplayName { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; init; } = string.Empty;
    }
}

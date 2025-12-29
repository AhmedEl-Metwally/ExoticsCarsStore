using Microsoft.AspNetCore.Identity;

namespace ExoticsCarsStoreServerSide.Domain.Models.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public Address? Address  { get; set; }
    }
}

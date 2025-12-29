using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ExoticsCarsStoreServerSide.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer(
                                            UserManager<ApplicationUser> _userManager,
                                            RoleManager<IdentityRole> _roleManager,
                                            ILogger<IdentityDataInitializer> _logger
                                        ) : IDataInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        DisplayName = "Ahmed Mohamed",
                        UserName = "AhmedMohamed",
                        Email = "AhmedMohamed@gmail.com",
                        PhoneNumber = "01091399362",
                    };
                    var User02 = new ApplicationUser
                    {
                        DisplayName = "Sharif Mohamed",
                        UserName = "SharifMohamed",
                        Email = "SharifMohamed@gmail.com",
                        PhoneNumber = "01070865586",
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while Seeding Database,{ex.Message} happened");

            }
        }
    }
}

using AdminDashboard.ViewModels.RoleViewModels;
using AdminDashboard.ViewModels.UserViewModels;
using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class UsersController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(User => new UserViewModel
            {
                Id = User.Id,
                DisplayName = User.DisplayName,
                Email = User.Email,
                Username = User.UserName,
                //Roles = _userManager.GetRolesAsync(User).Result
            }).ToListAsync();

            foreach (var user in users)
            {
                var appUser =  await _userManager.FindByIdAsync(user.Id);
                user.Roles = await _userManager.GetRolesAsync(appUser);
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();

            var userModel = new UserRoleViewModel
            {
                UserId = id,
                UserName = user.UserName!,
                Roles = roles.Select(R => new UpdateRoleViewModel
                {
                    Id = R.Id,
                    Name = R.Name!,
                    IsSelected = _userManager.IsInRoleAsync(user, R.Name!).Result
                }).ToList()
            };
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(R => R == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!userRoles.Any(R => R == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.Name);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

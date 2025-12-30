using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<ErrorToReturnValue<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (User is null)
                return ValidationErrorToReturn.InvalidCredentials("User.InvalidCredentials");

            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (!IsPasswordValid)
                return ValidationErrorToReturn.InvalidCredentials("User.InvalidCredentials");

            var Token = await CreateTokenAsync(User);
            return new UserDTO { Email = User.Email!, DisplayName = User.DisplayName, Token = Token };
        }

        public async Task<ErrorToReturnValue<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber
            };

            var IdentityResult = await _userManager.CreateAsync(User, registerDTO.Password);
            if (IdentityResult.Succeeded)
            {
                var Token = await CreateTokenAsync(User);
                return new UserDTO { Email = User.Email!, DisplayName = User.DisplayName, Token = Token };
            }

            return IdentityResult.Errors.Select(E => ValidationErrorToReturn.ValidationError(E.Code, E.Description)).ToList();
        }



        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));

            var SecretKey = _configuration["JwtOptions:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken
                (
                   issuer: _configuration["JwtOptions:Issuer"],
                   audience: _configuration["JwtOptions:Audience"],
                   claims: Claims,
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: Creds
                 );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }


    }
}

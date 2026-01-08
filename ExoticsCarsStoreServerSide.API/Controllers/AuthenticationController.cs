using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.Controllers
{
    public class AuthenticationController(IAuthenticationService _authenticationService) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var Result = await _authenticationService.LoginAsync(loginDTO);
            return HandleResult(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var Result = await _authenticationService.RegisterAsync(registerDTO);
            return HandleResult(Result);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync(string email)
        {
            var Result = await _authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUserAsync()
        {
            var Email = GetEmailFromToken();
            var Result = await _authenticationService.GetUserByEmailAsync(Email);
            return HandleResult(Result);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddressAsync()
        {
            var Email = GetEmailFromToken();
            var Result = await _authenticationService.GetAddressAsync(Email);
            return HandleResult(Result);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddressAsync(AddressDTO addressDTO)
        {
            var Email = GetEmailFromToken();
            var Result = await _authenticationService.UpdateUserAddressAsync(Email, addressDTO);
            return HandleResult(Result);
        }
    }
}

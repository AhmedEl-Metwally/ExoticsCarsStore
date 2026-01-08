using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var Result = await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return HandleResult(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var Result = await _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return HandleResult(Result);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync(string email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUserAsync()
        {
            var Email = GetEmailFromToken();
            var Result = await _serviceManager.AuthenticationService.GetUserByEmailAsync(Email);
            return HandleResult(Result);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddressAsync()
        {
            var Email = GetEmailFromToken();
            var Result = await _serviceManager.AuthenticationService.GetAddressAsync(Email);
            return HandleResult(Result);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddressAsync(AddressDTO addressDTO)
        {
            var Email = GetEmailFromToken();
            var Result = await _serviceManager.AuthenticationService.UpdateUserAddressAsync(Email, addressDTO);
            return HandleResult(Result);
        }
    }
}

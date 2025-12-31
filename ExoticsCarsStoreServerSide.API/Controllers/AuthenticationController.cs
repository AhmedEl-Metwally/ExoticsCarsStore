using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var Email = User.FindFirstValue(ClaimTypes.Email)!;
            var Result = await _authenticationService.GetUserByEmailAsync(Email);
            return HandleResult(Result);
        }
    }
}

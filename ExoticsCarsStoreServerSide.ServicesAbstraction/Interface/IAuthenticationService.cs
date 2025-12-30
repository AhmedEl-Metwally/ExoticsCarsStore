using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.IdentityDTOS;

namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface IAuthenticationService
    {
        Task<ErrorToReturnValue<UserDTO>> LoginAsync(LoginDTO loginDTO);
        Task<ErrorToReturnValue<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    }
}

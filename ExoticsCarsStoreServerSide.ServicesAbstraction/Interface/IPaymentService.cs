using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS;

namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface IPaymentService
    {
        Task<ErrorToReturnValue<BasketDTO>> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}

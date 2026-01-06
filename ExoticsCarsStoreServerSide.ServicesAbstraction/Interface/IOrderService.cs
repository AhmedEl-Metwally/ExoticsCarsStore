using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;

namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface IOrderService
    {
        Task<ErrorToReturnValue<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email);
        Task<ErrorToReturnValue<IEnumerable<DeliveryMethodDTO>>> GetAllDeliveryMethodAsync();
    }
}

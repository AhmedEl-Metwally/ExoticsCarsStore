using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.Controllers
{
    [Authorize]
    public class OrderController(IOrderService _orderService) : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO)
        {
            var orderToReturn = await _orderService.CreateOrderAsync(orderDTO, GetEmailFromToken());
            return HandleResult(orderToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrdersAsync()
        {
            var orderToReturn = await _orderService.GetAllOrdersAsync(GetEmailFromToken());
            return HandleResult(orderToReturn);
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdAsync(Guid Id)
        {
            var orderToReturn = await _orderService.GetOrderByIdAsync(Id);
            return HandleResult(orderToReturn);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethodAsync()
        {
            var deliveryMethod = await _orderService.GetAllDeliveryMethodAsync();
            return HandleResult(deliveryMethod);
        }
    }
}


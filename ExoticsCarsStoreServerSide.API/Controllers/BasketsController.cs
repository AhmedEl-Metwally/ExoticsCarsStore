using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS;
using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController(IBasketService _basketService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasketAsync(string id)
        {
            var Basket = await _basketService.GetBasketAsync(id);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basketDTO)
        {
            var Basket = await _basketService.CreateOrUpdateBasketAsync(basketDTO);
            return Ok(Basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
        {
            var Result = await _basketService.DeleteBasketAsync(id);
            return Ok(Result);
        }
    }
}

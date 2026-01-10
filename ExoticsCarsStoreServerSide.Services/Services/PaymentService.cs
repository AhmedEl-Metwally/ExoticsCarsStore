using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions;
using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = ExoticsCarsStoreServerSide.Domain.Models.ProductModule.Product;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class PaymentService(IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IConfiguration _configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<ErrorToReturnValue<BasketDTO>> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            var skey = _configuration["Stripe:SecretKey "];
            if (skey is null)
                return ValidationErrorToReturn.Failure("Failed to obtain Secret Ket Value");
            StripeConfiguration.ApiKey = skey;

            var basket = await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException("Basket not found");
            if (basket.DeliveryMethodId is null)
                return ValidationErrorToReturn.ValidationError("Delivery method is not selected");
            var method = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            if (method is null)
                return ValidationErrorToReturn.NotFound("Delivery method is not found");
            basket.ShippingPrice = method.Price;
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null)
                    return ValidationErrorToReturn.NotFound($"Product with id {item.Id} is not found");

                item.Price = product.Price;
                item.ProductName = product.Name;
                item.PictureUrl = product.PictureUrl;
            }
            long amount = (long)(basket.Items.Sum(I => I.Quantity * I.Price) * 100);

            var stripeService = new PaymentIntentService();
            if (basket.PaymentIntentId is null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = { "card" }
                };
                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions { Amount = amount };
                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return _mapper.Map<BasketDTO>(basket);
        }
    }
}

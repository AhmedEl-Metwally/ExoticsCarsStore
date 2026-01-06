using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions;
using ExoticsCarsStoreServerSide.Domain.Models.BasketModule;
using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Services.Specifications.OrderWithSpecifications;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<ErrorToReturnValue<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email)
        {
            var OrderAddress = _mapper.Map<OrderAddress>(orderDTO.Address);
            var Basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId) ?? throw new BasketNotFoundException(orderDTO.BasketId);
            //if (Basket is null)
            //    return ValidationErrorToReturn.NotFound("Basket.NotFound", $"The basket with Id:{orderDTO.BasketId} is Not found");

            List<OrderItem> OrderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                //if (product is null)
                //    return ValidationErrorToReturn.NotFound("Product.NotFound", $"The product with Id:{item.Id} is Not found");
                OrderItems.Add(CreateOrderItem(item, product));
            }

            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId)
                ?? throw new DeliveryMethodIdNotFoundException(orderDTO.DeliveryMethodId) ;
            //if (DeliveryMethod is null)
            //    return ValidationErrorToReturn.NotFound("DeliveryMethod.NotFound", $"The Delivery Method with this Id:{orderDTO.DeliveryMethodId} is Not Found ");

            var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order()
            {
                Address = OrderAddress,
                DeliveryMethod = DeliveryMethod,
                Items = OrderItems,
                SubTotal = SubTotal,
                UserEmail = Email
            };
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            int Result = await _unitOfWork.SaveChangesAsync();
            if(Result == 0)
                return ValidationErrorToReturn.Failure("Order.Failure", "There was a problem while creating the order");

            return _mapper.Map<OrderToReturnDTO>(order);
        }
     
        public async Task<ErrorToReturnValue<IEnumerable<DeliveryMethodDTO>>> GetAllDeliveryMethodAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync()
               ?? throw new DeliveryMethodStringNotFoundException("Not Delivery Method Found");
            var deliveryMethodsDto = _mapper.Map<IEnumerable<DeliveryMethodDTO>>(deliveryMethods);
            return ErrorToReturnValue<IEnumerable<DeliveryMethodDTO>>.Ok(deliveryMethodsDto);
        }

        public async Task<ErrorToReturnValue<IEnumerable<OrderToReturnDTO>>> GetAllOrdersAsync(string Email)
        {
            var spec = new OrderSpecifications(Email);
            var orders = await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec)?? throw new OrderByEmailNotFoundException(Email);
            var ordersDto = _mapper.Map<IEnumerable<OrderToReturnDTO>>(orders);
            return ErrorToReturnValue<IEnumerable<OrderToReturnDTO>>.Ok(ordersDto);
        }

        public Task<ErrorToReturnValue<OrderToReturnDTO>> GetOrderByIdAsync(Guid orderId)
        {
            var spec = new OrderSpecifications(orderId );
            var order = _unitOfWork.GetRepository<Order, Guid>() ?? throw new OrderByIdNotFoundException(orderId);
            var orderDto = _mapper.Map<OrderToReturnDTO>(order);
            return Task.FromResult(ErrorToReturnValue<OrderToReturnDTO>.Ok(orderDto));
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl
                },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }
    }
}

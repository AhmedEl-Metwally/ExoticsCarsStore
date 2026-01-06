using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;

namespace ExoticsCarsStoreServerSide.Services.Specifications.OrderWithSpecifications
{
    public class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string Email) : base(O => O.UserEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
        public OrderSpecifications(Guid Id) : base(O => O.Id == Id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}

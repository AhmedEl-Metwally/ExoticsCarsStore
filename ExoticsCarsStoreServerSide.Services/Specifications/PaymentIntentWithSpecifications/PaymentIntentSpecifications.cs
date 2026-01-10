using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;

namespace ExoticsCarsStoreServerSide.Services.Specifications.PaymentIntentWithSpecifications
{
    public class PaymentIntentSpecifications : BaseSpecifications<Order, Guid>
    {
        public PaymentIntentSpecifications(string PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}

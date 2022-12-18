using BeatStore.API.Helpers.Enums;

namespace BeatStore.API.Entities
{
    public class OrderDetails : BaseEntity
    {
        public string PaymentId { get; set; }
        public string Description { get; set; }
        public Currency? CurrencyCode { get; set; } = Currency.PLN;
        public PaymentMethod? PayMethod { get; set; } = null;
        public OrderStatus? Status { get; set; } = OrderStatus.PENDING;
        public string CustomerIP { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}

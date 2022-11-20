using BeatStore.API.Helpers.Enums;

namespace BeatStore.API.Entities
{
    public class OrderDetails : BaseEntity
    {
        public string PaymentId { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; } = "PLN";
        public PaymentMethod PayMethod { get; set; } = PaymentMethod.PBL;
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;
        public string CustomerIP { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}

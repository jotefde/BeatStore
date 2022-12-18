using BeatStore.API.DTO.PayU;

namespace BeatStore.API.DTO.Requests.Orders
{
    public class NotifyOrderRequest
    {
        public Order order { get; set; }
        public DateTime localReceiptDateTime { get; set; }
        public List<Property> properties { get; set; }
    }
}

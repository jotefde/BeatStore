using System.ComponentModel.DataAnnotations.Schema;

namespace BeatStore.API.Entities
{
    public class OrderAccess : BaseEntity
    {
        public OrderDetails OrderDetails { get; set; }
        [ForeignKey("OrderDetails")]
        public string OrderId { get; set; }
        public string Key { get; set; }
    }
}

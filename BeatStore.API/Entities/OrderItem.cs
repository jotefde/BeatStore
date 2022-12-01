using System.ComponentModel.DataAnnotations.Schema;

namespace BeatStore.API.Entities
{
    public class OrderItem : BaseEntity
    {
        public OrderDetails OrderDetails { get; set; }
        [ForeignKey("OrderDetails")]
        public string OrderDetailsId { get; set; }
        public Track Track { get; set; }
        [ForeignKey("Track")]
        public string TrackId { get; set; }
    }
}

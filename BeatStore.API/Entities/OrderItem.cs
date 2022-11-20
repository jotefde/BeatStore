namespace BeatStore.API.Entities
{
    public class OrderItem : BaseEntity
    {
        public OrderDetails OrderDetails { get; set; }
        public Track Track { get; set; }
    }
}

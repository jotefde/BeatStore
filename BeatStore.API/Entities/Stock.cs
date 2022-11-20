namespace BeatStore.API.Entities
{
    public class Stock : BaseEntity
    {
        public Track Track { get; set; }
        public int Amount { get; set; } = 0;
        public DateTime PublishTime { get; set; }
        public bool IsUnlimited { get; set; } = false;
    }
}

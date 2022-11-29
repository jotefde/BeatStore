namespace BeatStore.API.Entities
{
    public class Stock : BaseEntity
    {
        public Track Track { get; set; }
        public int? Amount { get; set; }
        public DateTime? PublishTime { get; set; }
        public bool? IsUnlimited { get; set; }
        public bool? IsPublished { get; set; }
    }
}

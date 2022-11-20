namespace BeatStore.API.Entities
{
    public class Track : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}

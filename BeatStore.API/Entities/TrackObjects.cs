using System.ComponentModel.DataAnnotations.Schema;

namespace BeatStore.API.Entities
{
    public class TrackObjects : BaseEntity
    {
        public Track Track { get; set; }
        [ForeignKey("Track")]
        public string TrackId { get; set; }
        public string WaveFile { get; set; }
        public string? SampleFile { get; set; }
        public string TrackoutFile { get; set; }
    }
}

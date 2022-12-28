using BeatStore.API.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeatStore.API.Entities
{
    public class TrackObject : BaseEntity
    {
        public virtual Track Track { get; set; }
        [ForeignKey("Track")]
        public string TrackId { get; set; }
        public string Name { get; set; }
        public TrackObjectType ObjectType { get; set; }
        public string MIME { get; set; }
        public long Size { get; set; }
    }
}

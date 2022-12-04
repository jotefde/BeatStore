using BeatStore.API.Extensions.RequestAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;
using System.Xml.Linq;

namespace BeatStore.API.DTO.Requests.Stock
{
    public class AddStockRequest
    {
        [Required]
        [GUID]
        public string TrackId { get; set; }
        public int Amount { get; set; } = 0;
        public bool IsUnlimited { get; set; } = true;
        public bool IsPublished { get; set; } = false;
    }
}

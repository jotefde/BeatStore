using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeatStore.API.DTO.Requests.Tracks
{
    public class AddTrackRequest
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name="Cover image file")]
        public IFormFile CoverImage { get; set; }
    }
}

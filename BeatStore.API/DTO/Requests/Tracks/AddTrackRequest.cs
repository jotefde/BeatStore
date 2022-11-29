using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeatStore.API.DTO.Requests.Tracks
{
    public class AddTrackRequest
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price cannot be empty")]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Required(ErrorMessage = "Description cannot be empty")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name="Cover image file")]
        public IFormFile CoverImage { get; set; }
    }
}

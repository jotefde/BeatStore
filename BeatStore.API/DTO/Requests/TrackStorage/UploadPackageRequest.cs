using BeatStore.API.Extensions.RequestAttributes;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeatStore.API.DTO.Requests.TrackStorage
{
    public class UploadPackageRequest
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(36, MinimumLength = 36)]
        public string TrackId { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".wav" })]
        public IFormFile WaveFile { get; set; }
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".mp3" })]
        public IFormFile? SampleFile { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".zip" })]
        public IFormFile TrackoutPack { get; set; }
    }
}

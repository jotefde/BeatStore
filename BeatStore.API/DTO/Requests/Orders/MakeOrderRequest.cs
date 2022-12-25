using BeatStore.API.Extensions.RequestAttributes;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Helpers.Validation;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeatStore.API.DTO.Requests.Orders
{
    public class MakeOrderRequest
    {
        public string Description { get; set; }
        [Required(ErrorMessage ="Currency code cannot be empty")]
        public Currency CurrencyCode { get; set; }
        [Required(ErrorMessage ="Email cannot be empty")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "First name cannot be empty")]
        [StringLength(maximumLength: 80, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string CustomerFirstName { get; set; }
        [Required(ErrorMessage = "Last name cannot be empty")]
        [StringLength(maximumLength: 80, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "Item list cannot be empty")]
        [GUID(true)]
        public IEnumerable<string> Items { get; set; }
    }
}

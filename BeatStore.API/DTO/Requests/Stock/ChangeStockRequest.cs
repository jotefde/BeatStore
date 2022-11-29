using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;
using System.Xml.Linq;

namespace BeatStore.API.DTO.Requests.Stock
{
    public class ChangeStockRequest
    {
        [Required(ErrorMessage = "Id is invalid")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 36, MinimumLength = 36)]
        public string StockId { get; set; }
        public int? Amount { get; set; }
        public bool? IsUnlimited { get; set; }
        public bool? IsPublished { get; set; }
    }
}

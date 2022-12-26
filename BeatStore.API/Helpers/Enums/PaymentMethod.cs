using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeatStore.API.Helpers.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentMethod
    {
        PBL,
        CARD_TOKEN,
        INSTALLMENTS
    }
}

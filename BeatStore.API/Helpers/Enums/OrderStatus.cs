using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeatStore.API.Helpers.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderStatus
    {
        PENDING,
        WAITING_FOR_CONFIRMATION,
        COMPLETED,
        CANCELED
    }
}

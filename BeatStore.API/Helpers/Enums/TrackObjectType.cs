using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeatStore.API.Helpers.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TrackObjectType
    {
        WAVE_FILE,
        TRACKOUT_FILE,
        SAMPLE_FILE,
    }
}

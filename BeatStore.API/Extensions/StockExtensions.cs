using BeatStore.API.Entities;

namespace BeatStore.API.Extensions
{
    internal static class StockExtensions
    {
        public static bool IsAvailable(this Stock source)
        {
            return source.IsPublished.Value && (source.IsUnlimited.Value || source.Amount.Value > 0);
        }
    }
}
